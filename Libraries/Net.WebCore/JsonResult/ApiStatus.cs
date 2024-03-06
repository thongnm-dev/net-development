using System.Runtime.Serialization;

namespace Net.WebCore.JsonResult
{
    public enum ApiStatus
    {
        [EnumMember(Value = "success")]
        SUCCESS,

        [EnumMember(Value = "error")]
        ERROR,

        [EnumMember(Value = "warning")]
        WARNING,

        [EnumMember(Value = "invalid")]
        INVALID,

        [EnumMember(Value = "bad-request")]
        BADREQUEST
    }
}
