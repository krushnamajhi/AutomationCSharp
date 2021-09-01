using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeywordDrivenProject.Models
{
    public static class DriverModal
    {
        static List<DriverTestCaseAssociationModal> driverRepos = new List<DriverTestCaseAssociationModal>();

        public static void add(DriverTestCaseAssociationModal driverTestCaseAssociationModal)
        {
            driverRepos.Add(driverTestCaseAssociationModal);
        }

        public static List<DriverTestCaseAssociationModal>  get()
        {
            return driverRepos;
        }

        public static DriverTestCaseAssociationModal getDriverRepos(int index)
        {
            return driverRepos[index];
        }

        public static DriverTestCaseAssociationModal getDriverAndTestCaseListByNodeId(int NodeId)
        {
            foreach (DriverTestCaseAssociationModal drv in driverRepos)
            {
                if (drv.Node == NodeId)
                    return drv;
                else
                    continue;
            }
            return null;
        }

        public  static IWebDriver getDriverByNodeId(int NodeId)
        {
            foreach (DriverTestCaseAssociationModal drv in driverRepos)
            {
                if (drv.Node == NodeId)
                    return drv.Driver;
                else
                    continue;
            }
            return null;
        }

        public static List<IWebDriver> getAllDrivers()
        {
            List<IWebDriver> driverList = new List<IWebDriver>();
            foreach (DriverTestCaseAssociationModal drv in driverRepos)
            {
                driverList.Add(drv.Driver);
            }
            return driverList;
        }

        public static void clearDriverRepository()
        {
            foreach(var drv in driverRepos)
            {
                drv.Driver.Quit();
            }
            driverRepos.Clear();
        }

        public static Boolean isDriverPresentForNode(int NodeId)
        {
            foreach (DriverTestCaseAssociationModal drv in driverRepos)
            {
                if (drv.Node == NodeId)
                    return true;
                else
                    continue;
            }
            return false;
        }

        public static void LoadDriverModal(List<IWebDriver> driverList, List<TestCaseModal> testCaseModalList)
        {
            int Node = 0;
            int startpoint = 0;
            int endPoint = 0;
            if (testCaseModalList.Count >= driverList.Count)
            {
                foreach (IWebDriver drv in driverList)
                {
                    DriverTestCaseAssociationModal dtcaObj = new DriverTestCaseAssociationModal();
                    dtcaObj.Driver = drv;
                    dtcaObj.Node = Node;
                    endPoint = (testCaseModalList.Count / driverList.Count) + startpoint;
                    for (int i = startpoint; i < endPoint; i++)
                    {
                        dtcaObj.TestCases.Add(testCaseModalList[i]);
                    }
                    startpoint = endPoint;
                    Node++;
                    add(dtcaObj);
                }
                if (testCaseModalList.Count % driverList.Count != 0)
                {
                    int Startpoint = testCaseModalList.Count - (testCaseModalList.Count % driverList.Count);
                    int endpoint = testCaseModalList.Count;
                    int driverIndex = 0;
                    for (int i = Startpoint; i < endpoint; i++)
                    {
                        getDriverRepos(driverIndex).TestCases.Add(testCaseModalList[i]);
                        driverIndex++;
                    }
                }
            }
            else
            {
                int endpoint = testCaseModalList.Count;
                for (int i = 0; i < endpoint; i++)
                {
                    DriverTestCaseAssociationModal dtcaObj = new DriverTestCaseAssociationModal();
                    dtcaObj.Driver = driverList[i];
                    dtcaObj.Node = i;
                    dtcaObj.TestCases.Add(testCaseModalList[i]);
                    add(dtcaObj);
                }
            }
        }
    }
}
