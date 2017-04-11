namespace Portal.Persistance.Members.Entities
{
    public class PhoneEntity
    {
        public string Number { get; set; }
        public MemberEntity Member { get; set; }
        public string MemberId { get; set; }
    }
}

