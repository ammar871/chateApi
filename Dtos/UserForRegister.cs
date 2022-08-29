namespace chatApi.Dtos{




      public class UserForRegister
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public int DriverId { get; set; }

        public string DeviceFCM { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}