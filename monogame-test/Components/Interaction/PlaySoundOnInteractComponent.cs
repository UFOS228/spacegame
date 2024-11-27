using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacegame.Components;
public class PlaySoundOnInteractComponent : InteractableComponent
{
    public PlaySoundOnInteractComponent(SoundEffectCollection snd, RandomGradient vol, RandomGradient pitchh, int interactId)
    {
        sound = snd;
        volume = vol;
        pitch = pitchh;
        myInteractId = interactId;
    }
    public SoundEffectCollection sound;
    public RandomGradient volume = 1;
    public RandomGradient pitch = 0;
    public override void onInteract(PlayerComponent player, int interactId)
    {
        sound.Play3DRandom(gameObject.position, volume, pitch);
    }
}

