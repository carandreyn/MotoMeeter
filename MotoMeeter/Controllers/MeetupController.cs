using Microsoft.AspNetCore.Mvc;
using MotoMeeter.Data.Enum;
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

        [HttpGet]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var meetups = category switch
            {
                -1 => await _meetupRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _meetupRepository.GetMeetupsByCategoryAndSliceAsync((MeetupCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _meetupRepository.GetCountAsync(),
                _ => await _meetupRepository.GetCountByCategoryAsync((MeetupCategory)category),
            };

            var viewModel = new IndexMeetupViewModel
            {
                Meetups = meetups,
                Page = page,
                PageSize = pageSize,
                TotalMeetups = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(viewModel);
        }

        [HttpGet]
        [Route("event/{MotorcycleMeetups}/{id}")]
        public async Task<IActionResult> DetailMeetup(int id, string motoMeetup)
        {
            var meetup = await _meetupRepository.GetByIdAsync(id);
            return meetup == null ? NotFound() : View(meetup);
        }

        [HttpGet]
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
                    MeetupCategory = meetupVM.MeetupCategory,
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

        [HttpGet]
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
                return View(meetupVM);
            }

            var userMeetup = await _meetupRepository.GetByIdAsyncNoTracking(id);

            if (userMeetup == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(meetupVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(meetupVM);
            }

            if (!string.IsNullOrEmpty(userMeetup.Image))
            {
                _ = _photoService.DeletePhotoAsync(userMeetup.Image);
            }

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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _meetupRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var meetupDetails = await _meetupRepository.GetByIdAsync(id);

            if (meetupDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(meetupDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(meetupDetails.Image);
            }

            _meetupRepository.Delete(meetupDetails);
            return RedirectToAction("Index");
        }
    }
}
