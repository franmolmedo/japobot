using System.Runtime.Serialization;

namespace FirstBasicBot.Translation
{
    [DataContract]
    public class AdmAccesToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }
        [DataMember(Name = "scope")]
        public string Scope { get; set; }

    }
}
