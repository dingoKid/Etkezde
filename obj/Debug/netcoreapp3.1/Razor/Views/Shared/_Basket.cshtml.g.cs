#pragma checksum "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "da95738b8b39a1b569ed9ca31feff761678b1e4a"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da95738b8b39a1b569ed9ca31feff761678b1e4a", @"/Views/Shared/_Basket.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"833904b026e077dbb04419730b89b985a41dfc56", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Basket : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<h3>Rendeles</h3>\r\n<table class=\"table table-condensed\" style=\"table-layout: fixed; width: 100%;\">\r\n    <thead>\r\n        <tr>\r\n            <th>Termek</th>\r\n            <th>Mennyiseg</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 12 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
     if(ViewBag.Basket != null)
    {
        foreach (KeyValuePair<string, int> item in ViewBag.Basket)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 17 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
               Write(item.Key);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 18 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
               Write(item.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n");
#nullable restore
#line 20 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n<input class=\"btn btn-success\" type=\"button\" value=\"Fizet\"");
            BeginWriteAttribute("onclick", " onclick=\"", 564, "\"", 633, 3);
            WriteAttributeValue("", 574, "location.href=\'", 574, 15, true);
#nullable restore
#line 24 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
WriteAttributeValue("", 589, Url.Action("OnPostFinishShopping", "Home"), 589, 43, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 632, "\'", 632, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n<input class=\"btn btn-danger\" type=\"button\" value=\"Torol\"");
            BeginWriteAttribute("onclick", " onclick=\"", 696, "\"", 762, 3);
            WriteAttributeValue("", 706, "location.href=\'", 706, 15, true);
#nullable restore
#line 25 "C:\Denes\c#\Etkezde\Views\Shared\_Basket.cshtml"
WriteAttributeValue("", 721, Url.Action("OnPostClearBasket", "Home"), 721, 40, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 761, "\'", 761, 1, true);
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