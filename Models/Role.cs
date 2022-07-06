 namespace MyBlog.Models
{
    public class Role
    {
        // используем код-конвенцию для настройки БД
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        // отношение один-ко-многим
        public Role()
        {
            Users = new List<User>();
        }
    }
}
