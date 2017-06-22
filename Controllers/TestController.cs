using Learner.Models;
using Learner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Learner.Controllers
{
    [EnableCors(origins: "http://127.0.0.1:8080", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserService _userService;

        public TestController(IReservationRepository reservationRepo, IUserService userService)
        {
            _reservationRepository = reservationRepo;
            _userService = userService;
            
        }
        public async Task<IEnumerable<User>> GetAllReservations()
        {

            HttpRequestHeaders header = this.Request.Headers;



            return await _userService.GetAllUsers();
        }

        public Reservation GetReservation(int id)
        {
            return _reservationRepository.Get(id);
        }

        public Reservation PostReservation(Reservation item)
        {
            return _reservationRepository.Add(item);
        }
        public bool PutReservation(Reservation item)
        {
            return _reservationRepository.Update(item);
        }
        public void DeleteReservation(int id)
        {
            _reservationRepository.Remove(id);
        }

    }
}
