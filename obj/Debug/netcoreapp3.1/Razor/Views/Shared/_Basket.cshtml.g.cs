#pragma checksum "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9f6e40c1996f96a49bef60d3ed8f0b3cfe7d3c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Basket), @"mvc.1.0.view", @"/Views/Shared/_Basket.cshtml")]
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
#line 1 "C:\Denes\c#\Etkezde\Views\_ViewImports.cshtml"
using Etkezde;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Denes\c#\Etkezde\Views\_ViewImports.cshtml"
using Etkezde.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9f6e40c1996f96a49bef60d3ed8f0b3cfe7d3c7", @"/Views/Shared/_Basket.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"833904b026e077dbb04419730b89b985a41dfc56", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Basket : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<table class=\"table table-condensed\" style=\"table-layout: fixed; width: 100%;\">\r\n    <thead>\r\n        <tr>\r\n            <th>Termek</th>\r\n            <th>Mennyiseg</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 9 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
     if(TempData["basket"] != null)
    {
        foreach (KeyValuePair<string, int> item in (Dictionary<string, int>) TempData["basket"])
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 14 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
               Write(item.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 15 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
               Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 17 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<input class=\"btn btn-success\" type=\"button\" value=\"Fizet\"");
            BeginWriteAttribute("onclick", " onclick=\"", 575, "\"", 644, 3);
            WriteAttributeValue("", 585, "location.href=\'", 585, 15, true);
#nullable restore
#line 21 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
WriteAttributeValue("", 600, Url.Action("OnPostFinishShopping", "Home"), 600, 43, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 643, "\'", 643, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n<input class=\"btn btn-danger\" type=\"button\" value=\"Torol\"");
            BeginWriteAttribute("onclick", " onclick=\"", 707, "\"", 773, 3);
            WriteAttributeValue("", 717, "location.href=\'", 717, 15, true);
#nullable restore
#line 22 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
WriteAttributeValue("", 732, Url.Action("OnPostClearBasket", "Home"), 732, 40, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 772, "\'", 772, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
