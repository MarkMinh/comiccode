using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using WebAPI.Models;

namespace WebAPI.CustomFormatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
        }
        protected override bool CanWriteType(Type? type)
        {
            return typeof(IEnumerable<Page>).IsAssignableFrom(type);
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var pages = (IEnumerable<Page>)context.Object;
            await using (var writer = new StreamWriter(response.Body, selectedEncoding))
            {
                foreach (var page in pages)
                {
                    writer.WriteLine($"Page id: {page.PageId}, page url: {page.PageImage}, page number: {page.PageNumber}");

                }

            }

        }
    }
}
