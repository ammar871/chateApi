namespace chatApi.Dtos
{
    public class CreateMessageDto
    {
              public int RoomId { get; set; }

         public string UserId { get; set; }
         public string SenderId { get; set; }

         public string MessageText { get; set; }

          public string Type { get; set; }
          public string TypeSender { get; set; }
          public int Status { get; set; }


    }
}