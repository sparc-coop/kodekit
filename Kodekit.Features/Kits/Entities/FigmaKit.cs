namespace Kodekit.Features.Kits.Entities;

public class FigmaKit
{
    public FigmaKit(KitRevision kit)
    {
        // Creating a Figma style from a Kodekit
        Buttons = new FigmaButtonStyle("abc-123", kit.Name + "-style", kit.Buttons.Font.Size.Value);
    }

    public KitRevision ApplyToKodekit(KitRevision kit)
    {
        // Applying Figma style to an existing Kodekit

        kit.Buttons.Font.Size.Value = Buttons.FontSize;
        return kit;
    }

    // Add Figma properties here (use records to make cleaner)
    public record FigmaButtonStyle(string Id, string Name, double FontSize);
    public record Color(string Id, string Name, string Value);
    public FigmaButtonStyle Buttons { get; set; }

}
