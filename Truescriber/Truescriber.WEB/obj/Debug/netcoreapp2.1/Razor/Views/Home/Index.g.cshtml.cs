#pragma checksum "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8aeea419ff16e473363680ad665b1766f3391aa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
#line 1 "E:\gitlab\Truescriber\Truescriber.WEB\Views\_ViewImports.cshtml"
using Truescriber.WEB;

#line default
#line hidden
#line 2 "E:\gitlab\Truescriber\Truescriber.WEB\Views\_ViewImports.cshtml"
using Truescriber.WEB.Models;

#line default
#line hidden
#line 1 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
using Truescriber.DAL.Entities;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8aeea419ff16e473363680ad665b1766f3391aa", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"94be69a006f42099bd4ebd41f28787b7b2ee2abf", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Truescriber.DAL.Entities.User>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(84, 192, true);
            WriteLiteral("\r\n\r\n<div class=\"bg-primary m-1 p-1 text-white\"><h2 align=\"center\">User Accounts</h2></div>\r\n<table class=\"table table-sm table-bordered\">\r\n    <tr><th>ID</th><th>Name</th><th>Email</th></tr>\r\n");
            EndContext();
#line 8 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
     if (Model.Count() == 0) {

#line default
#line hidden
            BeginContext(308, 76, true);
            WriteLiteral("        <tr><td colspan=\"3\" class=\"text-center\">No User Accounts</td></tr>\r\n");
            EndContext();
#line 10 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
    } else {
        foreach (User user in Model) {

#line default
#line hidden
            BeginContext(438, 38, true);
            WriteLiteral("            <tr>\r\n                <td>");
            EndContext();
            BeginContext(477, 7, false);
#line 13 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
               Write(user.Id);

#line default
#line hidden
            EndContext();
            BeginContext(484, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(512, 13, false);
#line 14 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
               Write(user.UserName);

#line default
#line hidden
            EndContext();
            BeginContext(525, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(553, 10, false);
#line 15 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
               Write(user.Email);

#line default
#line hidden
            EndContext();
            BeginContext(563, 27, true);
            WriteLiteral("</td>\r\n                <td>");
            EndContext();
            BeginContext(591, 11, false);
#line 16 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
               Write(user.Online);

#line default
#line hidden
            EndContext();
            BeginContext(602, 26, true);
            WriteLiteral("</td>\r\n            </tr>\r\n");
            EndContext();
#line 18 "E:\gitlab\Truescriber\Truescriber.WEB\Views\Home\Index.cshtml"
        }
    }

#line default
#line hidden
            BeginContext(646, 10, true);
            WriteLiteral("</table>\r\n");
            EndContext();
            BeginContext(656, 57, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "36c73d2842504cda88451cb3cca33905", async() => {
                BeginContext(703, 6, true);
                WriteLiteral("Create");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Truescriber.DAL.Entities.User>> Html { get; private set; }
    }
}
#pragma warning restore 1591
