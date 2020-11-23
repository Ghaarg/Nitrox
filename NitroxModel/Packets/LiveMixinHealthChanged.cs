﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using ProtoBufNet;

namespace NitroxModel.Packets
{
    [Serializable]
    [ProtoContract]
    public class LiveMixinHealthChanged : Packet
    {
        [Serializable]
        class DamageTakenData
        {
            public NitroxVector3 Position { get; set; }

            public ushort Damagetype { get; set; }

            public Optional<NitroxId> DealerId { get; set; }
        }
        Optional<DamageTakenData> damageTakenData;
        public NitroxTechType TechType { get; }

        public NitroxId Id { get; set; }

        public float LifeChanged { get; set; }

        
        public NitroxVector3 Position { get { return damageTakenData.HasValue ? damageTakenData.Value.Position : default; } }

        public ushort Damagetype { get { return damageTakenData.HasValue ? damageTakenData.Value.Damagetype : default; } }

        public Optional<NitroxId> DealerId { get { return damageTakenData.HasValue ? damageTakenData.Value.DealerId : default; } }

        public float TotalHealth { get; }

        public LiveMixinHealthChanged(NitroxTechType techType, NitroxId id, float lifeChanged, NitroxVector3 position, ushort damageType, Optional<NitroxId> dealerId, float totalHealth)
        {
            TechType = techType;
            Id = id;
            TotalHealth = totalHealth;
            LifeChanged = lifeChanged;
            if (lifeChanged < 0)
            {
                damageTakenData = new DamageTakenData
                {
                    Position = position,
                    Damagetype = damageType,
                    DealerId = dealerId
                };
            }
            else
            {
                damageTakenData = Optional.Empty;
            }
        }
    }
}
