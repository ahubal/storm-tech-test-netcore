using Newtonsoft.Json;

namespace Todo.Models.Users
{
    public class GravatarProfile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    public class GravatarEntry
    {
        [JsonProperty("entry")]
        public GravatarProfile[] Profiles { get; set; }
    }
}
