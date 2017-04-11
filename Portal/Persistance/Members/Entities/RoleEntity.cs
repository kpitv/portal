namespace Portal.Persistance.Members.Entities
{
    public class RoleEntity
    {
        public string Name { get; set; }
        public MemberEntity Member { get; set; }
        public string MemberId { get; set; }
    }
}