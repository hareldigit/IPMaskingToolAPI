using NLog;
using System.Threading.Tasks;

namespace MaskingService
{
    public class MaskingService
    {

        public void Execute()
        {

            //get file
            //read file rows into array of string (order by orig file rows) || fileRows<list>
            //V - create RegexIP function
            //V - create split ip function (host/endpoint)
            //create dictionary mapIps
            //V - create fakse ip'host' address function and create fakse ip 'endpoint' address function
            //dictionary<string,dictionary<string,list<string>>>
                        //host            endpont    rows index
            //loop on the fileRows
            //if contains ips
            //foreach ip
            //split as host & endpoint
            //push host into the first dictionary key
            //push endpoint into the second dictionary key
            //push the row index into the second dictionary value (if not exist)

            //loop on the dictionray structure
            //foreach host 
            //build fake host ip address (and check that is not exsit on the fakeused host address)
            //push the fake host ip into fakeused host
            //foreach end point
            //build fake end point ip address (and check that is not exsit on the fakeused endpoint address)
            //loop on rows indexs
            //replace the orig ip addrees with the fake ip
            //push the fake endpoint ip into fakeused host
        }
    }
}
