using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Templates
{
    interface ITemplate
    {
        List<Template> GetTemplates();
        List<Template> FilterByNameEmployee(string Name);
        Template GetTemplate(int Id, string table);
        bool ExistsId(int Id);
        int CreateTemplate(Template template);
        int UpdateTemplate(Template template);
        int DeleteTemplate(int Id);
        
    }
}
