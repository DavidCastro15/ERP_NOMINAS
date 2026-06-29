using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_NOMINAS.Models.Tunneling
{
    public interface ITunnel
    {
        List<Tunnel> GetTunnels();
        List<TunnelReport> GetDataReport(int PayWeek);
        List<Tunnel> FilterByValue(string Name);
        List<Tunnel> FilterByValue(int PayWeek);
    }
}
