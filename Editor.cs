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
        public void CopyAnims(AnimationsContainer omf_1, AnimationsContainer omf_2)
        {
            //bool omf_1.bone_cont.OGF_V != omf_2.bone_cont.OGF_V
            omf_1.RecalcAnimNum();

            foreach (AnimVector anim in omf_2.Anims)
            {
                omf_1.AddAnim(anim);
            }

            short b = (short)omf_1.AnimsCount;
            foreach (AnimationParams anim_param in omf_2.AnimsParams)
            {
                anim_param.MotionID = b;

                if((omf_1.bone_cont.OGF_V != omf_2.bone_cont.OGF_V) && omf_1.bone_cont.OGF_V == 3)
                {
                    anim_param.MarksCount = 0; 
                    anim_param.m_marks = null;
                }

                omf_1.AddAnimParams(anim_param);
                b++;
            }

            omf_1.RecalcAnimNum();
            omf_1.RecalcAllAnimIndex();
        }

        public void CopyAnims(AnimationsContainer omf_1, AnimationsContainer omf_2, List<string> list)
        {
            omf_1.RecalcAnimNum();

            short new_count = (short)omf_1.AnimsCount;

            for (int i = 0; i < omf_2.Anims.Count; i++)
            {
                AnimVector anim = omf_2.Anims[i];

                for (int ii = 0; ii < list.Count; ii++)
                {
                    if (anim.MotionName == list[ii])
                    {
                        omf_1.AddAnim(anim);

                        AnimationParams anim_param = omf_2.AnimsParams[i];

                        anim_param.MotionID = new_count;

                        if ((omf_1.bone_cont.OGF_V != omf_2.bone_cont.OGF_V) && omf_1.bone_cont.OGF_V == 3)
                        {
                            anim_param.MarksCount = 0;
                            anim_param.m_marks = null;
                        }

                        omf_1.AddAnimParams(anim_param);
                        new_count++;
                    }
                }
            }

            omf_1.RecalcAnimNum();
            omf_1.RecalcAllAnimIndex();

        }

        public void WriteOMF(BinaryWriter writer, AnimationsContainer omf_file)
        {

            omf_file.RecalcSectionSize();

            omf_file.WriteAnimationContainer(writer, this);

            omf_file.bone_cont.WriteBoneCont(writer, this);

            writer.Write(omf_file.AnimsParamsCount);

            foreach (AnimationParams anim_param in omf_file.AnimsParams)
                anim_param.WriteAnimationParams(writer, this, omf_file.bone_cont.OGF_V);
        }

        public AnimationsContainer OpenOMF(string filename)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                AnimationsContainer omf_file = new AnimationsContainer(reader, this);
                //Загрузка костей
                omf_file.bone_cont = new BoneContainer(reader, this);
                //Загрузка параметров анимаций
                omf_file.AnimsParamsCount = reader.ReadInt16();
                //Проверка
                if (omf_file.AnimsCount != omf_file.AnimsParamsCount) return null;

                for(int i = 0; i < omf_file.AnimsParamsCount; i++)
                {
                    AnimationParams anm_p = new AnimationParams(reader, this, omf_file.bone_cont.OGF_V);
                    omf_file.AddAnimParams(anm_p);
                }

                return omf_file;
            }
            
        }

        public int CompareOMF(AnimationsContainer omf_1, AnimationsContainer omf_2)
        {
            int error_v = 0;

            if (omf_1.bone_cont.Count != omf_2.bone_cont.Count)
            {
                return error_v = 1;
            }
            else
            {
                for (int i = 0; i < omf_1.bone_cont.Count; i++)
                {
                    if (omf_1.bone_cont.parts[i].Count != omf_2.bone_cont.parts[i].Count)
                    {
                        return error_v = 1;
                    }
                    else
                    {
                        for (int b = 0; b < omf_1.bone_cont.parts[i].Count; b++)
                        {
                            if(omf_1.bone_cont.parts[i].bones[b].Name != omf_2.bone_cont.parts[i].bones[b].Name)
                                return error_v = 1;
                        }
                    }
                }
            }

            if (omf_1.bone_cont.OGF_V != omf_2.bone_cont.OGF_V)
            {
                return error_v = 2;
            }
            else
                return error_v;
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
