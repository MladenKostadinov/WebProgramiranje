#pragma checksum "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5b732ffe54e84069b80870d63d330b6d31affdd6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Pages_InformacijeKnjiga), @"mvc.1.0.razor-page", @"/Pages/InformacijeKnjiga.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
using Projekat.Pages;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("RouteTemplate", "{IdKnjige}")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5b732ffe54e84069b80870d63d330b6d31affdd6", @"/Pages/InformacijeKnjiga.cshtml")]
    public class Pages_InformacijeKnjiga : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/Script.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Styles/Style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
  
    Layout = "_Layout";
    ViewData["Title"] = "Knjiga";


#line default
#line hidden
#nullable disable
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n  ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5b732ffe54e84069b80870d63d330b6d31affdd64218", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "5b732ffe54e84069b80870d63d330b6d31affdd65433", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("<div class=\"divZaKnjigu\">");
#nullable restore
#line 16 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                           
        if (Model.slika != null)
        {
            var base64 = Convert.ToBase64String(Model.slika);
            var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 517, "\"", 530, 1);
#nullable restore
#line 21 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 523, imgSrc, 523, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"slika\"");
            BeginWriteAttribute("onclick", " onclick=\"", 545, "\"", 585, 3);
            WriteAttributeValue("", 555, "promenaSlike(", 555, 13, true);
#nullable restore
#line 21 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 568, Model.knjiga.ID, 568, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 584, ")", 584, 1, true);
            EndWriteAttribute();
            WriteLiteral("/>\r\n");
#nullable restore
#line 22 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 642, "\"", 648, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"slika\"");
            BeginWriteAttribute("onclick", " onclick=\"", 663, "\"", 703, 3);
            WriteAttributeValue("", 673, "promenaSlike(", 673, 13, true);
#nullable restore
#line 25 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 686, Model.knjiga.ID, 686, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 702, ")", 702, 1, true);
            EndWriteAttribute();
            WriteLiteral(" alt=\"Nema slike za ovu knjigu!\"/>\r\n");
#nullable restore
#line 26 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
        }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("    \r\n\r\n    <div class=\"insideDiv\"><label class=\"lNaslov\"");
            BeginWriteAttribute("onclick", " onclick=\"", 819, "\"", 861, 3);
            WriteAttributeValue("", 829, "promenaNaslova(", 829, 15, true);
#nullable restore
#line 30 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 844, Model.knjiga.ID, 844, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 860, ")", 860, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 30 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                                                                                        Write(Model.knjiga.Naslov);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        <label class=\"lAutor\"");
            BeginWriteAttribute("onclick", " onclick=\"", 922, "\"", 963, 3);
            WriteAttributeValue("", 932, "promenaAutora(", 932, 14, true);
#nullable restore
#line 31 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 946, Model.knjiga.ID, 946, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 962, ")", 962, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 31 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                                                                   Write(Model.knjiga.Autor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        <label class=\"lIzdavac\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1025, "\"", 1068, 3);
            WriteAttributeValue("", 1035, "promenaIzdavaca(", 1035, 16, true);
#nullable restore
#line 32 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 1051, Model.knjiga.ID, 1051, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1067, ")", 1067, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 32 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                                                                       Write(Model.knjiga.Izdavac.Naziv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        <label class=\"lKolicina\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1139, "\"", 1182, 3);
            WriteAttributeValue("", 1149, "promenaKolicine(", 1149, 16, true);
#nullable restore
#line 33 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 1165, Model.knjiga.ID, 1165, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1181, ")", 1181, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Kolicina: ");
#nullable restore
#line 33 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                                                                                  Write(Model.knjiga.TrenKolicina);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n        <p class=\"par\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1252, "\"", 1296, 3);
            WriteAttributeValue("", 1262, "promenaParagrafa(", 1262, 17, true);
#nullable restore
#line 34 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
WriteAttributeValue("", 1279, Model.knjiga.ID, 1279, 16, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1295, ")", 1295, 1, true);
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 34 "C:\Users\mlade\Desktop\WEB\Projekat\backend\Pages\InformacijeKnjiga.cshtml"
                                                               Write(Model.knjiga.Opis);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<InformacijeKnjigaModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<InformacijeKnjigaModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<InformacijeKnjigaModel>)PageContext?.ViewData;
        public InformacijeKnjigaModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
