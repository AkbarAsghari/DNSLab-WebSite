namespace DNSLab.Shared;
partial class Modal
{
    bool IsOpen = false;

    [Parameter]
    public RenderFragment Title { get; set; }

    [Parameter]
    public RenderFragment Body { get; set; }

    [Parameter]
    public RenderFragment Footer { get; set; }

    [Parameter]
    public ModalSizeEnum ModalSize { get; set; } = ModalSizeEnum.Medium;

    private string _ModalSize
    {
        get
        {
            switch (ModalSize)
            {
                case ModalSizeEnum.XLarge:
                    return "xl";
                case ModalSizeEnum.Large:
                    return "lg";
                case ModalSizeEnum.Medium:
                default:
                    return "md";
                case ModalSizeEnum.Small:
                    return "sm";
            }
        }
    }


    public async Task Open()
    {
        IsOpen = true;
    }

    public async Task Close()
    {
        IsOpen = false;
    }
}
