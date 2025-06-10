using System;
using System.Collections.Generic;
using System.IO;
using System.AddIn;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aucotec.EngineeringBase.Client.Runtime;
using Aucotec.EngineeringBase.Core;
using System.Threading.Tasks;
using PlugIn3;


namespace EBPlugin
{
    [AddIn("oorja", Description = "DB Aggregation/Association Inspector", Publisher = "VishV")]
    public class MyPlugIn : PlugInWizard
    {
        public override void Run(Aucotec.EngineeringBase.Client.Runtime.Application myApplication)
        {
            string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "EB_DEBUG_LOG.txt");
            var loadingForm = new LoadingForm();  // Progress bar
            loadingForm.Show();

            loadingForm.BeginInvoke((Action)(() =>
            {
                try
                {
                    if (myApplication.ActiveProject == null)
                    {
                        MessageBox.Show("No active project detected!", "Error");
                        loadingForm.Close();
                        return;
                    }

                    StringBuilder sb = new StringBuilder();
                    IList<ObjectItem> allObjects = new List<ObjectItem>();
                    HashSet<ObjectItem> processedObjects = new HashSet<ObjectItem>();

                    CollectAllObjects(myApplication.ActiveProject, allObjects, processedObjects, 0);

                    HashSet<string> seenKinds = new HashSet<string>();

                    int counter = 0;
                    foreach (var obj in allObjects.Take(50))
                    {
                        counter++;
                        File.AppendAllText(logPath, $"[{DateTime.Now}] Processing object #{counter}: {obj.Name}\n");
                        ObjectKind kind = obj.Kind;
                        if (!seenKinds.Add(kind.ToString())) continue;

                        sb.AppendLine($"ObjectKind: {kind}");

                        myApplication.MetaData.AllowedAggregations(kind, out var aggs);
                        foreach (var a in aggs) sb.AppendLine($"  ├─ Aggregates: {a}");

                        myApplication.MetaData.AllowedSourceAssociations(kind, out var sources);
                        foreach (var s in sources) sb.AppendLine($"  ├─ Links TO: {s}");

                        myApplication.MetaData.AllowedTargetAssociations(kind, out var targets);
                        foreach (var t in targets) sb.AppendLine($"  ├─ Linked FROM: {t}");

                        sb.AppendLine();
                    }

                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "EB_Metadata_Structure.txt");
                    File.WriteAllText(filePath, sb.ToString());

                    loadingForm.Close();

                    var viewer = new MetadataViewerForm(sb.ToString(), filePath);
                    viewer.ShowDialog();
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logPath, $"[{DateTime.Now}] Exception: {ex.Message}\n");
                }

                File.AppendAllText(logPath, $"[{DateTime.Now}] Plugin ended.\n");
            }));
        }
        private void CollectAllObjects(ObjectItem parentObject, IList<ObjectItem> objectList, HashSet<ObjectItem> processedObjects, int depth = 0)
        {
            if (parentObject == null || parentObject.Children == null || processedObjects.Contains(parentObject) || depth > 10) return; // Depth limit

            processedObjects.Add(parentObject);
            objectList.Add(parentObject);

            foreach (ObjectItem child in parentObject.Children.OfType<ObjectItem>())
            {
                CollectAllObjects(child, objectList, processedObjects, depth + 1);
            }
        }

    }
}
