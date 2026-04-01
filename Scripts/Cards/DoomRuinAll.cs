using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Try.Scripts.Cards;


[Pool(typeof(NecrobinderCardPool))]
public class DoomRuinAll : CustomCardModel
{
    private const int energyCost = 0;

    private const CardType type = CardType.Attack;
    
    private const CardRarity rarity = CardRarity.Uncommon;
    
    private const TargetType target = TargetType.AllEnemies;
    
    private const bool  shouldShowInCardLibrary = true;
    
    
 /*
    protected override bool ShouldGlowGoldInternal => this.IsPlayable;

// 假设 Entry 名为 "DOOM"
// 这样直接通过类名获取对应的 ModelId 记录
    protected override bool IsPlayable => 
        this.Owner.Creature.HasPower(new ModelId("POWERS", ModelId.SlugifyCategory<DoomPower>()));
    
    */
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DoomPower>(20)
    ];
    
    
    public DoomRuinAll() : base(energyCost, type, rarity, target, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext,CardPlay cardPlay)
    {
        foreach (Creature creature in (IEnumerable<Creature>)this.Owner.Creature.CombatState.Creatures)
        {
            if (creature.IsAlive)
            {
                DoomPower doomPower = await PowerCmd.Apply<DoomPower>(creature,
                    this.DynamicVars.Doom.BaseValue, this.Owner.Creature, (CardModel)this);
            
                await DamageCmd.Attack(doomPower.Amount)
                    .FromCard(this)
                    .Targeting(creature)
                    .Execute(choiceContext);
            }
        }

        
        
    }
    
    
    protected override void OnUpgrade() => this.DynamicVars.Doom.UpgradeValueBy(10);

}