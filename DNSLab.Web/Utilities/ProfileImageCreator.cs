using Jdenticon;

namespace DNSLab.Web.Utilities
{
    public class ProfileImageCreator
    {
        public static string GenerateSVG(Guid userId, int size = 100) => GenerateSVG(userId.ToString(), size);

        public static string GenerateSVG(string value, int size = 100)
        {
            return Identicon.FromValue(value.ToUpper(), size: size)
            .ToSvg()
            .Replace("#ffffff", "none");//Remove White background;
        }
    }
}
