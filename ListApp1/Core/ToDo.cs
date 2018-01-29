using Realms;

namespace ListApp1.Core
{
    public class ToDo : RealmObject
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}