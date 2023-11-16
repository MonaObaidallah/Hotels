using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;



namespace Hotels.Controllers
{
    public class DashBoardController : Controller
    {
		private readonly ApplicationDbContext _context;
        List<Hotel> Hotels;
        public DashBoardController(ApplicationDbContext context)
        {
            _context = context;
			Hotels = new List<Hotel>();
		}

		public async Task<string> SendEmail()
		{
			var Message = new MimeMessage();
			Message.From.Add(MailboxAddress.Parse("mona.obaidallah@hotmail.com"));
			Message.Subject = "Test Email From My Project in Asp.net Core MVC";
			Message.Body = new TextPart("Plaint")
			{
				Text = "Welcome In My App"
			};

			using(var client=new SmtpClient())
			{
				try
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("mooncode33@gmail.com", "vdoznphtjotvvzlp");
					await client.SendAsync(Message);
					client.Disconnect(true);
				}
				catch(Exception e) 
				{
					return e.Message.ToString();
				};
			}
			return "Ok";
		}
		//vdoznphtjotvvzlp   باسورد تطبيق البريد

		[Authorize]
			public IActionResult Index()
        {
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.CurrentUser = currentuser;
            CookieOptions option = new CookieOptions();
            Response.Cookies.Append("UserName",currentuser,option);
            var hotel = _context.hotel.ToList();
			return View(hotel);
        }
        [HttpPost]
		public IActionResult Index(string city)
		{
			var hotel = _context.hotel.Where(x => x.City.Equals(city));
			return View(hotel);
		}

		public IActionResult CreateNewHotel(Hotel hotel)
		{
			_context.hotel.Add(hotel);
			_context.SaveChanges();
			return RedirectToAction("Index");

		}
		public IActionResult Delete(int id)
        {
            var hotledelete = _context.hotel.SingleOrDefault(x => x.id == id);
            _context.hotel.Remove(hotledelete);
            _context.SaveChanges();
			return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var hoteledit = _context.hotel.SingleOrDefault(x => x.id == id);
            return View(hoteledit);
        }
		public IActionResult Update(Hotel hotel)
		{
			if (ModelState.IsValid)
			{
				_context.hotel.Update(hotel);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View("Edit");

		}
		public IActionResult Rooms()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;
            ViewBag.currentuser = Request.Cookies["UserName"];
            var rooms = _context.rooms.ToList();
            return View(rooms);
        }
        
		public IActionResult CreateNewRoom(Rooms rooms)
		{
			_context.rooms.Add(rooms);
			_context.SaveChanges();
			return RedirectToAction("Rooms");

		}

        public IActionResult DeleteRoom(int Id)
        {
            var roomsdelete = _context.rooms.SingleOrDefault(r => r.Id == Id);
            _context.rooms.Remove(roomsdelete);
            _context.SaveChanges();
            return RedirectToAction("Rooms");
        }

        public IActionResult EditRooms(int Id)
		{
			var roomedit = _context.rooms.SingleOrDefault(r => r.Id == Id);
            ViewBag.hotel = _context.hotel.ToList();

            return View(roomedit);
		}
		public IActionResult UpdateRooms(Rooms rooms)
		{
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;
            if (ModelState.IsValid)
			{
				_context.rooms.Update(rooms);
				_context.SaveChanges();
				return RedirectToAction("Rooms");
			}
			return View("EditRooms");

		}

		public IActionResult RoomDetailes()
		{
			var roomDetails = _context.roomDetails.ToList();
			var hotel = _context.hotel.ToList();
			ViewBag.hotel = hotel;
			var rooms = _context.rooms.ToList();
			ViewBag.rooms = rooms;
			ViewBag.currentuser = Request.Cookies["UserName"];
			
			return View(roomDetails);
		}

		

		public IActionResult CreateNewRoomdetailes(RoomDetailes roomDetailes)
		{
			_context.roomDetails.Add(roomDetailes);
			_context.SaveChanges();
			return RedirectToAction("RoomDetailes");

		}

	}
}
