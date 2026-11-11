# SelfMade

**A solo-developed 2D puzzle platformer built with Unity, where death becomes part of the solution.**

Navigate platforms, climb ladders and dodge attacks announced by text warnings. When you die, your body stays behind and can activate platform switches while you respawn for another attempt.

## My Contribution

I developed the project independently: gameplay programming, code architecture, level assembly, and integration of animations, audio and game progression. **AI was used only to create the UI.

## Core Mechanics & Code

| System | Implementation | Key code |
| --- | --- | --- |
| Player controller | Movement, jumping, ladder climbing and moving-platform interaction using `Rigidbody2D` and the Input System. | [PlayerMovement](Assets/SelfMade/Components/Player/PlayerMovement.cs), [PlayerJump](Assets/SelfMade/Components/Player/PlayerJump.cs) |
| Death & respawn | Respawns the player while leaving a physical body in the level, including on moving platforms. | [PlayerLiveState](Assets/SelfMade/Components/Player/PlayerLiveState.cs) |
| Platforms & switches | Platforms follow timed routes; switches react to both the player and bodies. | [PlatformMoving](Assets/SelfMade/Components/Platform/PlatformMoving.cs), [ActivePlatformButton](Assets/SelfMade/Components/Platform/ActivePlatformButton.cs) |
| Telegraphing attacks | Text warning → aim at the player's captured height → launch → collision response. | [PhraseManager](Assets/SelfMade/Components/Phrase/PhraseManager.cs), [BoxPhrase](Assets/SelfMade/Components/Phrase/BoxPhrase.cs) |
| Level progression | Sequential goals, scene transitions and a dialogue-driven ending. | [GoalsManager](Assets/SelfMade/Components/Level/GoalsManager.cs), [LevelComponent](Assets/SelfMade/Components/Level/LevelComponent.cs) |

## Technical Highlights

- **Component-based design:** separate responsibilities for input, movement, jumping, player life state and audio.
- **C# events:** communicate jumps, respawns, goal completion and game-state changes.
- **ScriptableObject:** store per-level phrase data outside gameplay logic.
- **Audio pooling:** reuse `AudioSource` instances through [AudioService](Assets/SelfMade/Components/Core/AudioService.cs).
- **DOTween & coroutines:** coordinate platform motion, attack sequences, dialogue timing and visual feedback.

**Stack:** Unity 6 · C# · Physics 2D · Input System · URP · DOTween 

## Run

Clone or download the repository, open it in **Unity 6000.3.21f1**

**Controls:** `A/D` or `←/→` to move · `W/S` to climb · `Space` to jump.
