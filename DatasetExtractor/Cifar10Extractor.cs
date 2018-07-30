﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiaNet.Model;
using SiaNet.Common;
using System.IO;
using System.IO.Compression;

namespace DatasetExtractor
{
    internal class Cifar10Extractor : BaseExtractor, IExtractor
    {
        string folder = string.Format("{0}\\Cifar10", DefaultPath.Datasets);

        string[] urls = new string[] 
        {
            "https://www.cs.toronto.edu/~kriz/cifar-10-binary.tar.gz"
        };

        public void Download()
        {
            string tempFolder = folder + "\\tmp";
            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            foreach (var item in urls)
            {
                string filename = Path.GetFileName(item);
                string fullpath = tempFolder + "\\" + filename;

                if (File.Exists(fullpath))
                {
                    continue;
                }

                DownloadFile(item, fullpath);
            }
        }

        public void Extract()
        {
            //ExplodeZip();
            //string tempFolder = folder + "\\tmp";
            //TrainFrame = new XYFrame();
            //TestFrame = new XYFrame();
            //ExtractTrain(tempFolder);
            //ExtractTest(tempFolder);

            //TrainFrame.SaveStream(folder + "\\train.sia");
            //TestFrame.SaveStream(folder + "\\test.sia");

            //Directory.Delete(tempFolder, true);
        }

        private void ExtractTrain(string tmpfolder)
        {
            //for (int i = 1; i <= 5; i++)
            //{
            //    string filepath = string.Format("{0}\\cifar-10-batches-bin\\data_batch_{1}.bin", tmpfolder, i);
            //    FileStream imageStream = new FileStream(filepath, FileMode.Open);
            //    BinaryReader br = new BinaryReader(imageStream);
            //    int pixelSize = 32 * 32 * 3;

            //    List<byte> imageRec = null;

            //    for (int di = 0; di < 10000; ++di)
            //    {
            //        imageRec = new List<byte>();
            //        float lbl = br.ReadByte();

            //        imageRec.AddRange(br.ReadBytes(pixelSize));
            //        TrainFrame.XFrame.Data.Add(imageRec.Select(x => ((float)x)).ToList());
            //        TrainFrame.YFrame.Data.Add(new List<float>() { lbl });
            //    }

            //    imageStream.Close();
            //}
        }

        private void ExplodeZip()
        {
            string downloadedZipFile = folder + "\\tmp\\cifar-10-binary.tar.gz";
            //GZipStream gzStream = new GZipStream(new FileStream(downloadedZipFile, FileMode.Open), CompressionMode.Decompress);
            byte[] dataBuffer = new byte[4096];

            using (System.IO.Stream fs = new FileStream(downloadedZipFile, FileMode.Open, FileAccess.Read))
            {
                using (ICSharpCode.SharpZipLib.GZip.GZipInputStream gzipStream = new ICSharpCode.SharpZipLib.GZip.GZipInputStream(fs))
                {

                    // Change this to your needs
                    string fnOut = Path.Combine(folder + "\\tmp", Path.GetFileNameWithoutExtension(downloadedZipFile));

                    using (FileStream fsOut = File.Create(fnOut))
                    {
                        ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(gzipStream, fsOut, dataBuffer);
                    }
                }
            }

            downloadedZipFile = folder + "\\tmp\\cifar-10-binary.tar";
            
            using (System.IO.Stream fs = new FileStream(downloadedZipFile, FileMode.Open, FileAccess.Read))
            {
                ICSharpCode.SharpZipLib.Tar.TarArchive.CreateInputTarArchive(fs).ExtractContents(folder + "\\tmp");
            }
        }

        private void ExtractTest(string tmpfolder)
        {
            //string filepath = string.Format("{0}\\cifar-10-batches-bin\\test_batch.bin", tmpfolder);
            //FileStream imageStream = new FileStream(filepath, FileMode.Open);
            //BinaryReader br = new BinaryReader(imageStream);
            //int pixelSize = 32 * 32 * 3;

            //List<byte> imageRec = null;

            //for (int di = 0; di < 10000; ++di)
            //{
            //    imageRec = new List<byte>();
            //    float lbl = br.ReadByte();

            //    imageRec.AddRange(br.ReadBytes(pixelSize));
            //    TestFrame.XFrame.Data.Add(imageRec.Select(x => ((float)x)).ToList());
            //    TestFrame.YFrame.Data.Add(new List<float>() { lbl });
            //}

            //imageStream.Close();
        }
    }
}
