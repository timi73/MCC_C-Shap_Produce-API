#pragma checksum "D:\MCC\MCC_C-Shap_Produce-API\API\Client\Views\PageError\Unauthorized401.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b35a9d5b4ce9efc844fc2608e324607d09c237a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PageError_Unauthorized401), @"mvc.1.0.view", @"/Views/PageError/Unauthorized401.cshtml")]
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
#line 1 "D:\MCC\MCC_C-Shap_Produce-API\API\Client\Views\_ViewImports.cshtml"
using Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\MCC\MCC_C-Shap_Produce-API\API\Client\Views\_ViewImports.cshtml"
using Client.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b35a9d5b4ce9efc844fc2608e324607d09c237a", @"/Views/PageError/Unauthorized401.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3332004e6f18ccbec22253d7e177fe1fd5f40969", @"/Views/_ViewImports.cshtml")]
    public class Views_PageError_Unauthorized401 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\MCC\MCC_C-Shap_Produce-API\API\Client\Views\PageError\Unauthorized401.cshtml"
  
    ViewData["Title"] = "401 Unauthorized";
    Layout = "_LayoutErrorPage";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div id=""wrapper"">
    <div id=""content-wrapper"" class=""d-flex flex-column"">
        <div id=""content"">
            <div class=""container-fluid"">
                <div class=""text-center"">
                    <div class=""error mx-auto"" data-text=""401"">401</div>
                    <p class=""lead text-gray-800 mb-5"">Page Unauthorized</p>
                    <a href=""/Login"">&larr; Back to Dashboard</a>
                </div>
            </div>
        </div>
    </div>
</div>");
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
