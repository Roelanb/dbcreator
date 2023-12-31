using System.Text;

public class NavigationItem
{

    public string Header { get; set; }

    public string Template { get; set; }

    public List<NavigationItem>? ChildItems { get; set; }
}

public class Navigation
{
    public static List<NavigationItem> GetNavigation()
    {
        var navigation = new List<NavigationItem>();

        navigation.Add(new NavigationItem
        {
            Header = "Translations",
            Template = "ConfigTranslations",
            ChildItems = new List<NavigationItem>
            {
                new NavigationItem
                {
                    Header = "Applications",
                    Template = "ApplicationsEditor"
                },
                new NavigationItem
                {
                    Header = "Languages",
                    Template = "LanguagesEditor"
                },
                new NavigationItem
                {
                    Header = "Translation Types",
                    Template = "TranslationsTypesEditor"
                }
            }
        });

        navigation.Add(new NavigationItem
        {
            Header = "Quality",
            Template = "ConfigQuality",
            ChildItems = new List<NavigationItem>
            {
                new NavigationItem
                {
                    Header = "DefectQualityLevels",
                    Template = "DefectQualityLevelsEditor"
                },
                new NavigationItem
                {
                    Header = "DefectPieceRefs",
                    Template = "DefectPieceRefsEditor"
                }
            }
        });

        navigation.Add(new NavigationItem
        {
            Header = "MasterData",
            Template = "ConfigMasterData",
            ChildItems = new List<NavigationItem>
            {
                new NavigationItem
                {
                    Header = "LogoutIdleTimer",
                    Template = "LogoutIdleTimerEditor"
                },
                new NavigationItem
                {
                    Header = "ManufacturingType",
                    Template = "ManufacturingTypeEditor"
                }
            }
        });


        return navigation;
    }


    public static string GenerateNavigationFiles(string navFolder)
    {
        var navigation = GetNavigation();

        var topNavigationRazorString = GetTopNavigationRazor("CrudUi.Pages", navigation).ToString();

        File.WriteAllText(navFolder + "/" + "ConfigMain.razor", topNavigationRazorString);

        var topNavigationRazorCsString = GetTopNavigationRazorCs("CrudUi.Pages", navigation).ToString();

        File.WriteAllText(navFolder + "/" + "ConfigMain.razor.cs", topNavigationRazorCsString); 

        foreach (var navigationItem in navigation)
        {
            var sideNavigationRazorString = GetSideNavigationRazor("CrudUi.Pages", navigationItem, navigationItem.ChildItems).ToString();

            File.WriteAllText(navFolder + "/Config" + navigationItem.Header + ".razor", sideNavigationRazorString);

            var sideNavigationRazorCsString = GetSideNavigationRazorCs("CrudUi.Pages", navigationItem, navigationItem.ChildItems).ToString();

            File.WriteAllText(navFolder + "/Config" + navigationItem.Header + ".razor.cs", sideNavigationRazorCsString);
        }
       
        return "";
    }

    private static StringBuilder GetTopNavigationRazor(string uiNameSpace, List<NavigationItem> navigation)
    {

        var sb = new StringBuilder();

        sb.AppendLine("@page \"/config-main\"");
        sb.AppendLine($"@using {uiNameSpace}.Features.Config.Services");
        sb.AppendLine("@using Syncfusion.Blazor.Navigations");
        sb.AppendLine();
        sb.AppendLine("<div class=\"row\">");
        sb.AppendLine("    <div class=\"col-md-12\">");

        sb.AppendLine(" <SfTab CssClass=@(\"default-tab e-fill\")>");
        sb.AppendLine("     <TabAnimationSettings>");
        sb.AppendLine("         <TabAnimationPrevious Effect=AnimationEffect.None></TabAnimationPrevious>");
        sb.AppendLine("         <TabAnimationNext Effect=AnimationEffect.None></TabAnimationNext>");
        sb.AppendLine("     </TabAnimationSettings>");
        sb.AppendLine("     <TabItems>");

        foreach (var navigationItem in navigation)
        {
            sb.AppendLine("         <TabItem>");
            sb.AppendLine("             <ChildContent>");
            sb.AppendLine($"                 <TabHeader Text=\"{navigationItem.Header}\"></TabHeader>");
            sb.AppendLine("             </ChildContent>");
            sb.AppendLine("             <ContentTemplate>");
            sb.AppendLine($"                 <{navigationItem.Template}></{navigationItem.Template}>");
            sb.AppendLine("             </ContentTemplate>");
            sb.AppendLine("         </TabItem>");
        }

        sb.AppendLine("     </TabItems>");
        sb.AppendLine(" </SfTab>");

        sb.AppendLine("    </div>");
        sb.AppendLine("</div>");

        return sb;
    }

    private static StringBuilder GetTopNavigationRazorCs(string uiNameSpace, List<NavigationItem> navigation)
    {

        var sb = new StringBuilder();

        sb.AppendLine("using Microsoft.AspNetCore.Components;");
        sb.AppendLine();
        sb.AppendLine($"namespace {uiNameSpace}.Features.Config.Pages.Navigation;");    
        sb.AppendLine("public partial class ConfigMain : ComponentBase");
        sb.AppendLine("{");
        sb.AppendLine("     public string? ErrorMessage { get; set; }");
        sb.AppendLine();
        sb.AppendLine("     protected override async Task OnInitializedAsync()");
        sb.AppendLine("     {");
        sb.AppendLine("     ");
        sb.AppendLine("     }");
        sb.AppendLine();
        sb.AppendLine("}");
        
        return sb;

    }

    private static StringBuilder GetSideNavigationRazor(string uiNameSpace, NavigationItem parentItem, List<NavigationItem> navigation)
    {

        var sb = new StringBuilder();

        sb.AppendLine($"@page \"/config-{parentItem.Header.ToLower()}\"");
        sb.AppendLine($"@using {uiNameSpace}.Features.Config.Services");
        sb.AppendLine($"@using {uiNameSpace}.Features.Config.Pages.Editors");
        sb.AppendLine("@using Syncfusion.Blazor.Navigations");
        sb.AppendLine();

        sb.AppendLine("<div class=\"row\">");
        sb.AppendLine("    <div class=\"col-md-12\">");

        sb.AppendLine(" <SfTab CssClass=@(\"default-tab e-fill\") HeaderPlacement=\"HeaderPosition.Left\">");
        sb.AppendLine("     <TabAnimationSettings>");
        sb.AppendLine("         <TabAnimationPrevious Effect=AnimationEffect.None></TabAnimationPrevious>");
        sb.AppendLine("         <TabAnimationNext Effect=AnimationEffect.None></TabAnimationNext>");
        sb.AppendLine("     </TabAnimationSettings>");
        sb.AppendLine("     <TabItems>");

        foreach (var navigationItem in navigation)
        {
            sb.AppendLine("         <TabItem>");
            sb.AppendLine("             <ChildContent>");
            sb.AppendLine($"                 <TabHeader Text=\"{navigationItem.Header}\"></TabHeader>");
            sb.AppendLine("             </ChildContent>");
            sb.AppendLine("             <ContentTemplate>");
            sb.AppendLine($"                 <{navigationItem.Template}></{navigationItem.Template}>");
            sb.AppendLine("             </ContentTemplate>");
            sb.AppendLine("         </TabItem>");
        }

        sb.AppendLine("     </TabItems>");
        sb.AppendLine(" </SfTab>");

        sb.AppendLine("    </div>");
        sb.AppendLine("</div>");


        return sb;
    }
    private static StringBuilder GetSideNavigationRazorCs(string uiNameSpace, NavigationItem parentItem, List<NavigationItem> navigation)
    {

        var sb = new StringBuilder();

        sb.AppendLine("using Microsoft.AspNetCore.Components;");
        sb.AppendLine();
        sb.AppendLine($"namespace {uiNameSpace}.Features.Config.Pages.Navigation;");    
        sb.AppendLine($"public partial class Config{parentItem.Header} : ComponentBase");
        sb.AppendLine("{");
        sb.AppendLine("     public string? ErrorMessage { get; set; }");
        sb.AppendLine();
        sb.AppendLine("     protected override async Task OnInitializedAsync()");
        sb.AppendLine("     {");
        sb.AppendLine("     ");
        sb.AppendLine("     }");
        sb.AppendLine();
        sb.AppendLine("}");
        
        return sb;

    }

}