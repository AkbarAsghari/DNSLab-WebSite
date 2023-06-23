using Jdenticon;

namespace DNSLab.Helper.Utilities
{
    public class ProfileImageCreator
    {
        public static string GenerateSVG(Guid userId,int size = 0)
        {
            return Identicon.FromValue(userId, size: size == 0 ? 100 : size).ToSvg();
        }
    }
}
