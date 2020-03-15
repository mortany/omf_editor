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

            omf_file.RecalcSectionSize();

            omf_file.WriteAnimationContainer(writer, this);

            omf_file.bone_cont.WriteBoneCont(writer, this);

            writer.Write(omf_file.AnimsParamsCount);

            foreach (AnimationParams anim_param in omf_file.AnimsParams)
                anim_param.WriteAnimationParams(writer, this, omf_file.bone_cont.OGF_V);
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

                for(int i = 0; i < AnimCont.AnimsParamsCount; i++)
                {
                    AnimationParams anm_p = new AnimationParams(reader, this, AnimCont.bone_cont.OGF_V);
                    AnimCont.AddAnimParams(anm_p);
                }

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

        public void WriteMarkString(BinaryWriter writer, string text)
        {
            writer.Write(text.ToCharArray());
            writer.Write((byte)0x0D);
            writer.Write((byte)0x0A);
        }
    }
}
