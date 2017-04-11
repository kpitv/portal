namespace Portal.Persistance.Members.Entities
{
    public class ContactLinkEntity
    {
        public string Contact { get; set; }
        public string Link { get; set; }
        public MemberEntity Member { get; set; }
        public string MemberId { get; set; }
    }
}