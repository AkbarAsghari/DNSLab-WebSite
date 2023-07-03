using Microsoft.AspNetCore.Components.Web;
using System;

namespace DNSLab.Shared.Components.Tags;
partial class TagsInput
{
    string input;
    public void SubmitTag()
    {
        if (Tags == null)
            Tags = new List<string>();

        IsContainSpecialCharacter = false;

        if (!String.IsNullOrEmpty(input)
        && !Tags.Exists(t => t.Equals(input, StringComparison.CurrentCultureIgnoreCase)))
        {
            Tags.Add(input);
        }

        input = String.Empty;
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
