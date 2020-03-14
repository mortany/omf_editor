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
    }

    public class AnimVector
    {
        public int    SectionId;
        public uint   SectionSize;
        public string Name;
        public byte[] data;

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
    }

    public class AnimationParams
    {
        public string Name;
        public int Flags;
        public short BoneOrPart;
        public short MotionID;
        public float Speed;
        public float Power;
        public float Accrue;
        public float Falloff;
        public int MarksCount;

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

            if(motion_version == 4)
            {
                MarksCount = reader.ReadInt32();

                if(MarksCount > 0)
                {
                    m_marks = new List<MotionMark>();

                    for (int i = 0; i < MarksCount; i++)
                    {
                        MotionMark newmark = new MotionMark();

                        newmark.Name = editor.ReadSuperString(reader);
                        newmark.Count = reader.ReadInt32();

                        for (int n = 0; n < newmark.Count; n++)
                        {
                            MotionMarkParams param = new MotionMarkParams();
                            param.t0 = reader.ReadSingle();
                            param.t1 = reader.ReadSingle();
                            newmark.m_params.Add(param);
                        }

                        m_marks.Add(newmark);
                    }
                }
            }
            else
            {
                MarksCount = -1;
            }

        }
    }

    public class MotionMark
    {
        public string Name;
        public int    Count;

        public List<MotionMarkParams> m_params = new List<MotionMarkParams>();
    }

    public class MotionMarkParams
    {
        public float t0;
        public float t1;
    }
}
