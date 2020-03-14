using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OMF_Editor
{
    public class OMFEditor
    {
        public void WriteOMF(BinaryWriter writer, AnimationsContainer omf_file)
        {
            writer.Write(omf_file.SectionId);
            writer.Write(omf_file.SectionSize);
            writer.Write(omf_file.SectionId2);
            writer.Write(omf_file.SectionSize2);
            writer.Write(omf_file.AnimsCount);

            foreach (AnimVector anim in omf_file.Anims)
            {
                writer.Write(anim.SectionId);
                writer.Write(anim.SectionSize);
                WriteSuperString(writer, anim.Name);
                writer.Write(anim.data);
            }
        }

        public bool OpenOMF(string filename, List<AnimationsContainer> OMFFiles)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                AnimationsContainer AnimCont = new AnimationsContainer(reader, this);
                //Загрузка костей
                AnimCont.bone_cont = new BoneContainer(reader, this);

                AnimCont.AnimsParamsCount = reader.ReadInt16();
                //Проверка
                if (AnimCont.AnimsCount != AnimCont.AnimsParamsCount) return false;

                

                OMFFiles.Add(AnimCont);

                return true;
            }
            
        }

        public string ReadSuperString(BinaryReader reader)
        {

            string str = "";

            while (true)
            {
                byte b;

                b = reader.ReadByte();

                if (b == 0 || b == 10 || b == 13)
                {
                    if (b == 13) reader.ReadByte();
                    return str;
                }
                else
                {
                    str += Convert.ToChar(b).ToString();
                }

            }
        }

        public void WriteSuperString(BinaryWriter writer, string text)
        {
            writer.Write(text.ToCharArray());
            writer.Write((byte)0);
        }
    }
}
