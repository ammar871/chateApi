namespace chatApi.Dtos
{
    public class CreateRoomDto
    {

        
        public string Message { get; set; }

        public string SenderId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
         public int Status { get; set; }
        
    }
}

