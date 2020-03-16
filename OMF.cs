using System;
using System.Collections.Generic;
using System.IO;

namespace OMF_Editor
{
    public class AnimationsContainer
    {
        public int SectionId;
        public uint SectionSize;

        public int SectionId2;
        public uint SectionSize2;

        public int AnimsCount;
        public short AnimsParamsCount;

        public List<AnimVector> Anims = new List<AnimVector>();
        public List<AnimationParams> AnimsParams = new List<AnimationParams>();

        public void RecalcSectionSize()
        {
            uint new_size = 0;
            foreach(AnimVector anim in Anims)
            {
                anim.RecalcSectionSize();
                new_size += anim.GetSize();
            }
            new_size += 12;
            SectionSize = new_size;
        }

        public void RecalcAnimNum()
        {
            AnimsCount = AnimsParams.Count;
            AnimsParamsCount = (short)AnimsParams.Count;
        }

        public void RecalcAllAnimIndex()
        {
            short i = 0;
            foreach(AnimationParams anm in AnimsParams)
            {
                anm.MotionID = i;
                i++;
            }
        }

        public BoneContainer bone_cont;

        public void AddAnim(AnimVector vector)
        {
            Anims.Add(vector);
        }

        public void AddAnimParams(AnimationParams param)
        {
            AnimsParams.Add(param);
        }
        
        public short GetMotionVersion()
        {
            return bone_cont.OGF_V;
        }

        public AnimationsContainer(BinaryReader reader, OMFEditor editor)
        {
            SectionId = reader.ReadInt32();
            SectionSize = reader.ReadUInt32();

            SectionId2 = reader.ReadInt32();
            SectionSize2 = reader.ReadUInt32();

            AnimsCount = reader.ReadInt32();

            int count = AnimsCount;

            for (int i = 0; i < count; i++)
            {
                AnimVector vector = new AnimVector
                {
                    SectionId = reader.ReadInt32(),
                    SectionSize = reader.ReadUInt32(),
                    Name = editor.ReadSuperString(reader)
                };

                int size = (int)vector.SectionSize - (vector.Name.Length + 1);

                vector.data = reader.ReadBytes(size);

                AddAnim(vector);
            }
        }

        public void WriteAnimationContainer(BinaryWriter writer, OMFEditor editor)
        {
            writer.Write(SectionId);
            writer.Write(SectionSize);

            writer.Write(SectionId2);
            writer.Write(SectionSize2);

            writer.Write(AnimsCount);

            foreach (AnimVector anim in Anims)
            {
                writer.Write(anim.SectionId);
                writer.Write(anim.SectionSize);
                editor.WriteSuperString(writer, anim.Name);
                writer.Write(anim.data);
            }
        }
    }

    public class AnimVector
    {
        public int    SectionId;
        public uint   SectionSize;
        public string Name;
        public byte[] data;

        public uint GetSize()
        {
            return SectionSize + 8;
        }

        public void RecalcSectionSize()
        {
            SectionSize = (uint)(Name.Length + 1 + data.Length);
        }

        public string MotionName
        {
            get { return Name; }
        }
    }

    public class BoneParts
    {
        public string Name;
        public short Count;

        public List<BoneVector> bones = new List<BoneVector>();
    }

    public class BoneVector
    {
        public string Name;
        public uint ID;
    }

    public class BoneContainer
    {
        public int SectionId;
        public uint SectionSize;
        public short OGF_V;
        public short Count;

        public List<BoneParts> parts = new List<BoneParts>();

        public BoneContainer(BinaryReader reader, OMFEditor editor)
        {
            SectionId = reader.ReadInt32();
            SectionSize = reader.ReadUInt32();
            OGF_V = reader.ReadInt16();
            Count = reader.ReadInt16();

            for (int i = 0; i < Count; i++)
            {
                BoneParts bonesparts = new BoneParts
                {
                    Name = editor.ReadSuperString(reader),
                    Count = reader.ReadInt16()
                };

                for (int n = 0; n < bonesparts.Count; n++)
                {
                    BoneVector sbone = new BoneVector
                    {
                        Name = editor.ReadSuperString(reader),
                        ID = reader.ReadUInt32()
                    };
                    bonesparts.bones.Add(sbone);
                }

                parts.Add(bonesparts);
            }
        }

        public void WriteBoneCont(BinaryWriter writer, OMFEditor editor)
        {
            writer.Write(SectionId);
            writer.Write(SectionSize);
            writer.Write(OGF_V);
            writer.Write(Count);

            foreach (BoneParts bone in parts)
            {
                editor.WriteSuperString(writer, bone.Name);
                writer.Write(bone.Count);

                foreach (BoneVector sbone in bone.bones)
                {
                    editor.WriteSuperString(writer, sbone.Name);
                    writer.Write(sbone.ID);
                }
            }
        }
    }

    public class MotionMark
    {
        public string Name;
        public int    Count;

        public List<MotionMarkParams> m_params = new List<MotionMarkParams>();

        public MotionMark(BinaryReader reader, OMFEditor editor)
        {
            Name = editor.ReadSuperString(reader);
            Count = reader.ReadInt32();

            for (int n = 0; n < Count; n++)
            {
                MotionMarkParams param = new MotionMarkParams(reader);
                m_params.Add(param);
            }
        }

        public void WriteMotionMark(BinaryWriter writer, OMFEditor editor)
        {
            editor.WriteMarkString(writer, Name);
            writer.Write(Count);

            foreach(MotionMarkParams param in m_params)
            {
                writer.Write(param.t0);
                writer.Write(param.t1);
            }
        }
    }

    public class MotionMarkParams
    {
        public float t0;
        public float t1;
        public MotionMarkParams(BinaryReader reader)
        {
            t0 = reader.ReadSingle();
            t1 = reader.ReadSingle();
        }
    }

    public class AnimationParams
    {
        public string Name { get; set; }
        public int Flags { get; set; }
        public short BoneOrPart { get; set; }
        public short MotionID { get; set; }
        public float Speed { get; set; }
        public float Power { get; set; }
        public float Accrue { get; set; }
        public float Falloff { get; set; }
        public int MarksCount { get; set; }

        public List<MotionMark> m_marks; // = new List<MotionMark>();

        public AnimationParams(BinaryReader reader, OMFEditor editor, short motion_version)
        {
            Name = editor.ReadSuperString(reader);
            Flags = reader.ReadInt32();
            BoneOrPart = reader.ReadInt16();
            MotionID = reader.ReadInt16();
            Speed = reader.ReadSingle();
            Power = reader.ReadSingle();
            Accrue = reader.ReadSingle();
            Falloff = reader.ReadSingle();
            MarksCount = 0;

            if (motion_version == 4)
            {
                MarksCount = reader.ReadInt32();

                if (MarksCount > 0)
                {
                    m_marks = new List<MotionMark>();

                    for (int i = 0; i < MarksCount; i++)
                    {
                        MotionMark newmark = new MotionMark(reader, editor);
                        m_marks.Add(newmark);
                    }
                }
            }
        }

        public void WriteAnimationParams(BinaryWriter writer, OMFEditor editor, short motion_version)
        {
            editor.WriteSuperString(writer, Name);
            writer.Write(Flags);
            writer.Write(BoneOrPart);
            writer.Write(MotionID);
            writer.Write(Speed);
            writer.Write(Power);
            writer.Write(Accrue);
            writer.Write(Falloff);

            if (motion_version != 4) return;

            writer.Write(MarksCount);

            if(MarksCount != 0 && m_marks != null)
            {
                foreach (MotionMark mark in m_marks)
                    mark.WriteMotionMark(writer, editor);
            }

        }

    }
}
