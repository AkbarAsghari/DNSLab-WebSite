using Microsoft.AspNetCore.Components.Web;
using System;

namespace DNSLab.Shared.Components.Tags;
partial class TagsInput
{
    string input;
    protected string Value
    {
        get
        {
            return input;
        }
        set
        {
            if (Tags == null)
                Tags = new List<string>();

            IsContainSpecialCharacter = false;

            if (!String.IsNullOrEmpty(value))
            {
                if (value.EndsWith(",") || value.EndsWith("،"))
                {
                    value = Value.Replace(",", String.Empty).Replace("،", String.Empty).Trim();

                    if (!String.IsNullOrEmpty(value)
                    && !Tags.Exists(t => t.Equals(value, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        Tags.Add(value);
                    }

                    value = string.Empty;
                }
            }

            input = value ?? string.Empty;
        }
    }
    protected bool IsContainSpecialCharacter { get; set; }
    [Parameter] public List<string> Tags { get; set; }

    protected void DeleteTag(string value)
    {
        if (String.IsNullOrEmpty(value)) return;

        var tag = Tags.FirstOrDefault(t => t == value);
        if (tag == null) return;

        Tags.Remove(tag);
    }
}
