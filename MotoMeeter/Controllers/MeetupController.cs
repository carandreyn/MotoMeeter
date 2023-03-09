using Microsoft.AspNetCore.Mvc;
using MotoMeeter.Interfaces;
using MotoMeeter.Models;
using MotoMeeter.Repository;
using MotoMeeter.ViewModels;

namespace MotoMeeter.Controllers
{
    public class MeetupController : Controller
    {
        private readonly IMeetupRepository _meetupRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeetupController(IMeetupRepository meetupRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _meetupRepository = meetupRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Meetup> meetups = await _meetupRepository.GetAll();
            return View(meetups);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Meetup meetup = await _meetupRepository.GetByIdAsync(id);
            return View(meetup);
        }

        public IActionResult Create()
        {
            var curUserID = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createMeetupViewModel = new CreateMeetupViewModel { AppUserId = curUserID };
            return View(createMeetupViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMeetupViewModel meetupVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(meetupVM.Image);

                var meetup = new Meetup
                {
                    Title = meetupVM.Title,
                    Description = meetupVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = meetupVM.AppUserId,
                    Address = new Address
                    {
                        Street = meetupVM.Address.Street,
                        City = meetupVM.Address.City,
                        State = meetupVM.Address.State,
                    }
                };
                _meetupRepository.Add(meetup);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(meetupVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var meetup = await _meetupRepository.GetByIdAsync(id);
            if (meetup == null) return View("Error");
            var meetupVM = new EditMeetupViewModel
            {
                Title = meetup.Title,
                Description = meetup.Description,
                AddressId = meetup.AddressId,
                Address = meetup.Address,
                URL = meetup.Image,
                MeetupCategory = meetup.MeetupCategory
            };
            return View(meetupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMeetupViewModel meetupVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit meetup");
                return View("Edit", meetupVM);
            }

            var userMeetup = await _meetupRepository.GetByIdAsyncNoTracking(id);

            if (userMeetup != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userMeetup.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(meetupVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(meetupVM.Image);


                var meetup = new Meetup
                {
                    Id = id,
                    Title = meetupVM.Title,
                    Description = meetupVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = meetupVM.AddressId,
                    Address = meetupVM.Address,
                };

                _meetupRepository.Update(meetup);

                return RedirectToAction("Index");
            }
            else
            {
                return View(meetupVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var meetupDetails = await _meetupRepository.GetByIdAsync(id);
            if (meetupDetails == null) return View("Error");
            return View(meetupDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var meetupDetails = await _meetupRepository.GetByIdAsync(id);
            if (meetupDetails == null) return View("Error");

            _meetupRepository.Delete(meetupDetails);
            return RedirectToAction("Index");
        }
    }
}
