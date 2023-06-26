using Jdenticon;

namespace DNSLab.Helper.Utilities
{
    public class ProfileImageCreator
    {
        public static string GenerateSVG(Guid userId, int size = 100)
        {
            return Identicon.FromValue(userId, size: size).ToSvg();
        }
    }
}
