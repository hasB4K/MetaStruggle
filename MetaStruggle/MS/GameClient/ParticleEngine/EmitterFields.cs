﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using GameClient.IO;
using DPSF;

namespace GameClient.ParticleEngine
{
    public class EmitterFields
    {
        public bool Enabled { get; set; }
        public bool EmitParticlesAutomatically { get; set; }
        public bool LerpEmittersPositionAndOrientation { get; set; }
        public bool LerpEmittersPositionAndOrientationOnNextUpdate { get; set; }
        public float ParticlesPerSecond { get; set; }
        public int BurstParticles { get; set; }
        public float BurstTime { get; set; }

        public static void CopyEmitterFieldsToParticleEmitter(EmitterFields copyEmitter, ParticleEmitter emitter)
        {
            emitter.Enabled = copyEmitter.Enabled;
            emitter.EmitParticlesAutomatically = copyEmitter.EmitParticlesAutomatically;
            emitter.LerpEmittersPositionAndOrientation = copyEmitter.LerpEmittersPositionAndOrientation;
            emitter.LerpEmittersPositionAndOrientationOnNextUpdate =
                copyEmitter.LerpEmittersPositionAndOrientationOnNextUpdate;
            emitter.ParticlesPerSecond = copyEmitter.ParticlesPerSecond;
            emitter.BurstParticles = copyEmitter.BurstParticles;
            emitter.BurstTime = copyEmitter.BurstTime;
        }
    }
}
