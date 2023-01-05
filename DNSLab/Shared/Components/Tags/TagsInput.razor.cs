using Microsoft.AspNetCore.Components.Web;

namespace DNSLab.Shared.Components.Tags;
partial class TagsInput
{
    protected string Value { get; set; }
    protected bool MenuVisibility { get; set; }
    protected bool IsContainSpecialCharacter { get; set; }
    [Parameter] public List<string> Tags { get; set; }

    protected void AddTags(KeyboardEventArgs eventArgs)
    {
        if (Tags == null)
            Tags = new List<string>();

        IsContainSpecialCharacter = false;

        if (!String.IsNullOrEmpty(Value))
        {
            if (eventArgs.Key.Equals(",") || Value.EndsWith(",") || eventArgs.Key.Equals("،") || Value.EndsWith("،"))
            {
                Value = Value.Replace(",", String.Empty).Replace("،", String.Empty).Trim();

                if (!String.IsNullOrEmpty(Value)
                && !Tags.Exists(t => t.Equals(Value, StringComparison.CurrentCultureIgnoreCase)))
                {
                    Tags.Add(Value);
                }

                Value = string.Empty;
            }
        }
    }

    protected void DeleteTag(string value)
    {
        if (String.IsNullOrEmpty(value)) return;

        var tag = Tags.FirstOrDefault(t => t == value);
        if (tag == null) return;

        Tags.Remove(tag);
    }
}
