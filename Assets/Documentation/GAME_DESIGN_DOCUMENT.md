# Alley Fursuit

## Game Design Document

| Document Field | Value |
|---|---|
| Version | 1.0 |
| Authors | Alley Fursuit Development Team |
| Last Updated | July 2, 2026 |
| Project Deadline | July 31, 2026 |
| Document Status | Preproduction baseline |

> **Document convention:** Items marked **Placeholder** or **TBD** require validation during implementation or playtesting. Unless otherwise stated, numeric tuning values are starting points rather than final balance targets.

---

## Revision History

| Version | Date | Author | Summary |
|---|---|---|---|
| 1.0 | July 2, 2026 | Alley Fursuit Development Team | Initial production GDD based on the approved project concept and available asset library. |

---

## Table of Contents

1. [Game Overview](#1-game-overview)
2. [Vision Statement](#2-vision-statement)
3. [Story & Narrative](#3-story--narrative)
4. [Core Gameplay](#4-core-gameplay)
5. [Gameplay Mechanics](#5-gameplay-mechanics)
6. [Characters](#6-characters)
7. [World Design](#7-world-design)
8. [Level Progression](#8-level-progression)
9. [Controls](#9-controls)
10. [UI / HUD](#10-ui--hud)
11. [Art Direction](#11-art-direction)
12. [Audio Design](#12-audio-design)
13. [Technical Design](#13-technical-design)
14. [Asset List](#14-asset-list)
15. [Team Structure](#15-team-structure)
16. [Development Timeline](#16-development-timeline)
17. [Risks](#17-risks)
18. [Future Improvements](#18-future-improvements)
19. [Appendix](#appendix)

---

# 1. Game Overview

## High Concept

**Alley Fursuit** is a short, fast-paced 2D platformer in which an orange cat pursues a thieving mouse through an alley, factory, and sewer. Players combine precise jumping, double jumping, and dashing to overcome hazards, fend off interfering animals, and recover a prized fish skeleton.

## Elevator Pitch

Pip has stolen Mochi's favorite fish skeleton and is getting away. Chase him across three compact, obstacle-dense stages, mastering an expressive movement kit while rival cats and mischievous mice turn a simple pursuit into twenty minutes of escalating platforming chaos.

## Product Profile

| Category | Specification |
|---|---|
| Genre | 2D action-platformer |
| Target Audience | Casual and intermediate platformer players; ages 10+ |
| Platform | PC (Windows) |
| Estimated Playtime | Approximately 20 minutes for a first successful run |
| Engine | Unity 6.5 (6000.5.1f1) |
| Perspective | 2D side-scrolling |
| Players | Single-player |
| Progression | Linear, three levels |
| Scope | One playable character, two standard enemy families, three levels, two collectible types, no inventory, no dialogue, no boss, and one ending |
| Business Model | **TBD**; assumed free academic/portfolio release for current scope |

## Design Pillars

1. **Momentum with control:** Movement should feel quick, responsive, and recoverable. The player—not a rhythm track—sets the pace.
2. **Readable challenge:** Hazards, enemies, and safe landing spaces must be identifiable at a glance.
3. **Compact escalation:** Each level introduces a distinct environmental challenge, then combines learned skills in the finale.
4. **Playful pursuit:** Animation, staging, and collectible placement continually reinforce that Mochi is chasing Pip.

## Scope Boundary

The July 31 release prioritizes a polished, complete twenty-minute experience. Procedural levels, combat depth, inventory, dialogue, bosses, online features, branching routes, and multiple endings are out of scope unless all core milestones are complete ahead of schedule.

---

# 2. Vision Statement

The intended experience is a lighthearted chase that makes the player feel agile, determined, and just barely in control. Mochi should respond immediately to input, build satisfying forward momentum, and retain enough midair control for mistakes to feel correctable rather than arbitrary.

Challenge comes from reading the environment and executing movement—not memorizing inputs against music. Although **Geometry Dash** is a reference for energetic pacing, strong silhouettes, and rapid obstacle escalation, **Alley Fursuit** is deliberately not rhythm-based. Players control Mochi's horizontal movement, choose when to jump or dash, explore optional collectible paths, and approach platforming situations at their own pace.

The project follows three production principles:

- Prefer a small number of polished mechanics over many shallow features.
- Teach mechanics safely before testing them under pressure.
- Preserve clarity at speed through consistent visual and audio feedback.

---

# 3. Story & Narrative

## Premise

In a bright, exaggerated industrial district, the clever mouse Pip steals Mochi's prized fish skeleton and flees into the city. Mochi gives chase through a neighborhood alley, an active factory, and the sewers beneath it. Pip stays one step ahead, enlisting other mice and exploiting environmental hazards while rival cats join the pursuit for their own reasons.

## Main Character

**Mochi** is an energetic orange cat and the player's avatar. The stolen fish skeleton is a sentimental prize and a simple, visually readable goal. Mochi communicates through movement, facial expressions, and short nonverbal vocalizations; there is no written or spoken dialogue in the launch scope.

## Antagonist

**Pip** is a clever, mischievous mouse who drives the chase. Pip is not a boss encounter. He appears in brief environmental sightings, scripted escapes, and the final recovery sequence, serving as a moving objective rather than a combat target.

## Setting

The game takes place over one continuous pursuit through three connected spaces:

1. A colorful urban alley that introduces traversal.
2. A noisy factory filled with machinery and timed hazards.
3. A damp sewer network that combines prior mechanics with tighter timing.

The setting favors cartoon logic and readable exaggeration over realism.

## Player Motivation

- **Immediate:** Keep moving toward Pip and survive the current obstacle sequence.
- **Short-term:** Reach the next checkpoint and collect visible coins or power cells.
- **Long-term:** Complete all three levels and recover Mochi's fish skeleton.
- **Optional mastery:** Improve completion time and collectible totals. **Placeholder:** final results-screen metrics are subject to schedule.

## Ending

At the end of the sewers, Mochi corners Pip and recovers the fish skeleton. Pip escapes unharmed through a small drain, leaving open the possibility of another chase. Mochi celebrates with the fish skeleton as the victory screen summarizes the run. The story has one ending and contains no dialogue.

---

# 4. Core Gameplay

## Core Gameplay Loop

1. Read the next platforming challenge.
2. Move, jump, double jump, or dash through obstacles.
3. Avoid hazards and manage enemy encounters.
4. Collect coins and optional power cells.
5. Reach a checkpoint, recover from failure if necessary, and continue the pursuit.
6. Reach the level exit and advance to the next environment.

## Player Goals

| Priority | Goal |
|---|---|
| Primary | Reach Pip at the end of the final level. |
| Secondary | Preserve lives by avoiding hazards and enemies. |
| Optional | Collect coins and power cells placed on standard and higher-risk routes. |

## Win Condition

The player wins by reaching the final chase trigger in Level 3 and completing the scripted fish-skeleton recovery sequence.

## Lose Condition

Taking damage or touching a lethal hazard causes a life loss according to the health rules below. The game-over condition occurs when no lives remain. The player may restart from the beginning of the current level. **Placeholder:** whether the last checkpoint can be retained after game over must be validated against target difficulty.

---

# 5. Gameplay Mechanics

## Movement

Mochi can move left and right with immediate acceleration and limited inertia. The character should be easy to reposition on small platforms while still conveying speed during longer runs.

| Parameter | Initial Target |
|---|---|
| Walk/run model | Single analog speed range; keyboard reaches full speed |
| Horizontal speed | **TBD through prototype testing** |
| Ground acceleration | Fast, approximately 0.1 seconds to full speed |
| Ground deceleration | Fast, slightly softer than acceleration |
| Air control | Approximately 70% of ground control; **Placeholder** |
| Facing | Automatically follows horizontal movement |

Running has no stamina cost. Slopes, moving platforms, and one-way platforms use standard Unity 2D behavior where included in the final levels.

## Jump

Jump is the primary traversal action. Jump height should support both short taps and full-height presses through variable jump height.

- A jump begins only while grounded, with a small **coyote-time** window after leaving a ledge.
- A short input-buffer window stores a jump pressed just before landing.
- Releasing jump early reduces upward velocity for a shorter arc.
- **Initial tuning placeholder:** 0.10 seconds coyote time and 0.10 seconds input buffer.

## Double Jump

Mochi may jump once more while airborne. The double jump refreshes after touching stable ground or a valid moving platform.

- The second jump is available after walking off a ledge, unless playtesting finds this too forgiving.
- A distinct effect and sound communicate its use.
- The double jump partially resets downward velocity to ensure consistent recovery.
- It does not refresh the dash unless explicitly changed during tuning.

## Dash

Dash provides a short burst in the current facing direction and supports gap crossing, hazard avoidance, and advanced route optimization.

| Rule | Initial Design |
|---|---|
| Direction | Horizontal only |
| Availability | Once while airborne; refreshes on landing |
| Duration / Distance | **TBD during prototype** |
| Cooldown | **Placeholder:** no ground cooldown beyond dash duration |
| Invulnerability | None by default |
| Input during dash | Limited steering; jump input may queue near dash end |

Dash must never imply invulnerability unless clear visual feedback is added. Power cells may interact with dash recovery in a future iteration, but the launch behavior is **TBD**.

## Collision

Mochi uses a compact capsule or rounded box collider sized to the body rather than the full sprite. Collision layers separate the player, terrain, hazards, enemies, collectibles, checkpoints, and triggers.

- Solid terrain blocks movement.
- One-way platforms allow upward passage and top landing.
- Hazard contact invokes damage or instant life loss, depending on hazard type.
- Moving platforms transfer stable horizontal motion to the player.
- Enemy contact uses dedicated hitboxes so visual animation does not unpredictably change collision.

## Health System

The brief defines **3 Lives**; the launch baseline treats these as lives rather than three hit points.

- Normal enemy contact or minor hazards remove one life and respawn Mochi at the latest checkpoint.
- Bottomless pits, crushing hazards, and unrecoverable falls also remove one life.
- A short post-respawn grace period prevents immediate repeated damage.
- **Placeholder:** if multi-hit health is preferred, add a separate three-heart health pool only after usability testing; do not run both systems without clear UI distinction.

## Lives

The player begins a new game with three lives. Remaining lives persist between levels. Extra lives are not currently collectible. When lives reach zero, the game-over screen appears.

## Checkpoints

Checkpoints divide each level into short, recoverable sections. On activation, a checkpoint records Mochi's respawn position and visually changes state.

- Target spacing: 30–60 seconds of first-attempt play between checkpoints.
- Respawn resets local enemies and hazards to a valid authored state.
- Collected items before the checkpoint remain collected during that attempt.
- Save persistence beyond the active session is covered in Technical Design.

## Collectibles

| Collectible | Purpose | Placement Strategy | Status |
|---|---|---|---|
| Coins | Optional score and route guidance | Trails along safe lines; clusters on riskier paths | Confirmed; final score values **TBD** |
| Power Cells | High-value collectible associated with industrial machinery | Off the critical path or behind movement challenges | Confirmed; gameplay reward **TBD** |

Collectibles should guide the eye, preview jump arcs, and reward controlled exploration without requiring backtracking. There is no inventory. Collected totals appear on the HUD and results screens.

## Enemy Encounters

Enemies create movement problems rather than deep combat encounters. **Placeholder:** the primary defeat action is landing on an enemy's vulnerable top hitbox. If stomp combat proves unreliable, enemies will function as avoidable hazards instead.

- Enemy states remain simple: idle/patrol, alert, attack or chase, defeated/reset.
- Contact from a dangerous side causes life loss.
- Stomping a vulnerable enemy defeats or temporarily disables it and gives Mochi a small bounce.
- Enemy placement must leave a readable avoidance path.
- There are no weapons, ammunition, combos, or boss fights.

---

# 6. Characters

## Mochi

| Attribute | Description |
|---|---|
| Species / Appearance | Orange cat with a readable silhouette and expressive animation |
| Role | Playable protagonist |
| Description | A small but determined cat pursuing the thief who stole a prized fish skeleton. |
| Personality | Energetic, stubborn, curious, and easily distracted by shiny objects—but relentless once focused. |
| Abilities | Walk/run, jump, double jump, horizontal dash, enemy stomp (**Placeholder**) |
| Visual Feedback | Idle, run, jump/fall, double-jump, dash, hit, defeat, and victory states |

## Pip

| Attribute | Description |
|---|---|
| Species / Appearance | Small mouse with the fish skeleton or an identifiable theft cue |
| Role | Non-combat antagonist and chase target |
| Description | A clever mouse who remains just ahead of Mochi and uses the environment to delay pursuit. |
| Personality | Mischievous, resourceful, confident, and theatrical rather than cruel. |
| Behavior | Appears at scripted sightlines, triggers or foreshadows obstacles, then exits toward the next objective. Pip cannot be damaged during normal play. |

## Enemy Cats

Rival cats compete to catch Pip and obstruct Mochi. They emphasize pursuit and territorial movement.

| Element | Design |
|---|---|
| Primary Behavior | Patrol a platform, then briefly charge when Mochi enters a forward detection zone |
| Player Response | Jump over, bait the charge, or stomp if that mechanic is retained |
| Readability | Larger silhouette, clear wind-up animation, distinct warning sound |
| Debut | Late Level 1 or early Level 2; **TBD based on asset fit** |

## Enemy Mice

Pip's allies use small size and environmental positioning to slow Mochi.

| Element | Design |
|---|---|
| Primary Behavior | Short patrol or stationary obstacle throw; final implementation **TBD** |
| Player Response | Time a jump, dash past, or stomp if vulnerable |
| Readability | Contrasting color from Pip; attack anticipation before any projectile or lunge |
| Debut | Level 1, after basic movement tutorialization |

Pip must remain visually distinct from all enemy mice through color, accessory, animation, or the visible fish skeleton.

---

# 7. World Design

## Level 1 – Alley

**Purpose:** Teach the complete movement kit in a forgiving environment and establish the chase.

| Category | Design |
|---|---|
| Objectives | Learn movement, jump, double jump, and dash; activate checkpoints; follow Pip into the factory district. |
| Obstacles | Crates, trash bins, short gaps, fire escapes, puddles or minor hazards, and simple moving platforms (**Placeholder**). |
| Enemies | Enemy mice first; one introductory rival cat encounter if scope permits. |
| Collectibles | Coin trails demonstrate safe routes; one or more power cells teach optional-route language. Exact count **TBD**. |
| Visual Style | Bright sunset alley, brick walls, storefront silhouettes, pipes, fences, signs, and layered industrial skyline. |
| Difficulty Goal | Low; each mechanic is introduced in isolation with safe recovery space. |

The opening uses environmental staging instead of text-heavy instruction where possible. Input prompts appear only when a mechanic is first required.

## Level 2 – Factory

**Purpose:** Test timing and introduce moving, cyclical hazards.

| Category | Design |
|---|---|
| Objectives | Pursue Pip through production spaces, navigate machinery, and reach the drainage access. |
| Obstacles | Conveyor belts, pistons, presses, steam vents, rotating machinery, electrified gaps, and longer dash jumps. Final hazard set **TBD** after prototype feasibility. |
| Enemies | Enemy mice positioned around machinery; rival cats using longer patrol and charge lanes. |
| Collectibles | Coins indicate moving-platform timing; power cells occupy detours near machinery. Exact count **TBD**. |
| Visual Style | Saturated factory interior, large readable machines, warm hazard lighting, cool structural layers, and parallax industrial depth. |
| Difficulty Goal | Medium; combine two mechanics at a time and shorten recovery windows. |

Hazards use predictable cycles and visual anticipation. Players should be able to understand a machine before committing to its danger zone.

## Level 3 – Sewers

**Purpose:** Deliver the final movement exam and resolve the chase.

| Category | Design |
|---|---|
| Objectives | Track Pip through the drainage network, survive the final gauntlet, and recover the fish skeleton. |
| Obstacles | Narrow platforms, flowing water, dripping hazards, breakaway or moving platforms (**Placeholder**), vertical shafts, and combined dash/double-jump sequences. |
| Enemies | Faster or more strategically placed variants of enemy cats and mice; no new enemy family. |
| Collectibles | Collectible lines reward mastery and create optional high-risk routes without blocking completion. Exact count **TBD**. |
| Visual Style | Teal-green waterways, warm utility lights, pipes, brick tunnels, reflections, and industrial parallax layers. |
| Difficulty Goal | High but fair; checkpointed sequences combine mechanics learned in Levels 1 and 2. |

The final level may combine all previous mechanics—including conveyors, moving machinery, enemy timing, double jumps, and dashes—rather than introducing a major new system. The last segment transitions into a short, readable chase set piece and scripted ending.

---

# 8. Level Progression

| Progression Phase | Player Learning | Challenge Structure |
|---|---|---|
| Alley: Introduction | Basic movement and jump | Wide platforms, low consequence, single hazards |
| Alley: Expansion | Double jump and dash | Larger gaps, optional collectible routes |
| Factory: Application | Timing against moving systems | Predictable hazard cycles and enemy overlap |
| Factory: Pressure | Chaining abilities | Longer sequences with fewer idle spaces |
| Sewers: Mastery | Selecting the correct movement tool | Tighter geometry and combined mechanics |
| Sewers: Finale | Sustained execution | Short final gauntlet, generous last checkpoint, chase payoff |

Difficulty ramps through combination and reduced reaction time, not through hidden rules or excessive punishment. Every new hazard follows a three-step pattern: safe preview, isolated test, then combination with known elements. Checkpoint density increases around difficult sequences so the cost of learning remains low.

Target first-clear pacing is approximately 5 minutes for the Alley, 7 minutes for the Factory, and 8 minutes for the Sewers, including a reasonable number of retries.

---

# 9. Controls

| Action | Keyboard | Gamepad | Notes |
|---|---|---|---|
| Move | `A` / `D` or Left / Right Arrow | Left Stick or D-Pad | Full horizontal control |
| Jump / Double Jump | `Space` | South face button | Press again while airborne for double jump |
| Dash | Left `Shift` | East or West face button; **TBD** | Horizontal dash in facing direction |
| Pause | `Esc` | Menu / Start | Opens pause menu |
| Navigate UI | Arrow Keys / Mouse | D-Pad / Left Stick | |
| Confirm | `Enter` / Left Click | South face button | |
| Back | `Esc` / Right Click | East face button | |

Input prompts use the Kenney Input Prompts pack where compatible. Rebinding is a **Placeholder** feature: expose it only if Unity's Input System and settings schedule allow complete implementation and persistence.

---

# 10. UI / HUD

The interface is minimal, high-contrast, and subordinate to the playfield. UI supports keyboard, mouse, and gamepad navigation with a visible selection state.

## Main Menu

- Game title and key art or animated scene.
- **Play / Continue** (wording depends on save implementation).
- **Settings**.
- **Credits**.
- **Quit**.

## Pause Menu

- Resume.
- Restart from Checkpoint.
- Restart Level.
- Settings.
- Return to Main Menu, with confirmation if progress may be lost.

Gameplay simulation and timers pause while this menu is open.

## HUD

| Element | Placement | Behavior |
|---|---|---|
| Lives | Top-left | Cat-head or heart icons showing remaining lives |
| Coins | Top-right | Coin icon and collected count |
| Power Cells | Near coin counter | Cell icon and collected/available count; **Placeholder** |
| Dash State | Near Mochi or HUD edge | **TBD:** show only if dash availability is not readable from character VFX |
| Tutorial Prompt | Contextual lower area | Appears on first use, then fades |

## Game Over Screen

- “Game Over” heading.
- Restart Level.
- Return to Main Menu.
- Current collectible totals; **Placeholder** depending on persistence rules.

## Victory Screen

- Illustration or animation of Mochi recovering the fish skeleton.
- Completion time.
- Coins and power cells collected.
- Replay Game / Main Menu.
- Credits access.

## Settings Menu

- Master, music, and sound-effect volume.
- Fullscreen/windowed mode.
- Resolution selection.
- Input reference; rebinding **TBD**.
- Accessibility options: screen shake toggle and reduced flashing; implementation target subject to schedule.

---

# 11. Art Direction

## Visual Identity

The game uses a clean cartoon aesthetic built primarily from Kenney assets. Shapes are bold, colors are bright, and interactive objects remain readable against industrial backgrounds. The mood is playful and kinetic rather than grim, even in the factory and sewer.

## Art Principles

- **Bright palette:** Warm oranges identify Mochi; environment palettes shift by level without reducing character contrast.
- **Readable silhouettes:** Characters, enemies, platforms, hazards, and collectibles remain recognizable at gameplay scale.
- **Consistent asset treatment:** Imported packs use a shared pixels-per-unit strategy, filtering mode, sorting conventions, and color grading.
- **Industrial parallax:** Background layers create depth but move slowly and use lower contrast than collision-bearing foregrounds.
- **Minimal UI:** Flat icons, limited panels, generous spacing, and no unnecessary ornamentation.

## Level Palettes

| Level | Dominant Palette | Accent / Hazard Color |
|---|---|---|
| Alley | Warm brick, sky blue, muted purple | Yellow signage and red hazards |
| Factory | Steel blue, charcoal, warm orange | Red/orange machinery warnings |
| Sewers | Teal, moss green, deep navy | Yellow utility lights and toxic lime |

Animation should prioritize anticipation and state readability over frame count. Dash, damage, checkpoint, collectible, and death states require clear VFX even if character animation remains lightweight.

---

# 12. Audio Design

## Background Music

Energetic instrumental music supports the pursuit without controlling gameplay timing. Each level should have a distinct arrangement or track:

- **Alley:** playful, upbeat introduction.
- **Factory:** mechanical percussion and increased intensity.
- **Sewers:** tense, driving rhythm with a final-resolution cue.

Music loops must transition cleanly and leave frequency space for gameplay cues.

## Sound Effects

| Event | Direction |
|---|---|
| Jump | Light, springy movement cue |
| Double Jump | Brighter layered variation of jump |
| Dash | Short air-slice or burst with strong onset |
| Hit | Clear impact that does not sound graphic |
| Collect | Distinct coin and power-cell tones |
| Explosion | Cartoon industrial impact; used sparingly |
| Death / Life Lost | Brief descending cue followed by quick respawn feedback |
| Checkpoint | Positive activation chime |
| Enemy Alert | Short warning matched to anticipation animation |
| UI | Consistent hover, confirm, and back cues |

## Future Audio Improvements

- Adaptive music layers based on chase intensity.
- Surface-specific footsteps and landings.
- Ambient loops for alley traffic, factory machinery, and sewer water.
- Positional enemy cues and additional accessibility mixing.
- Nonverbal vocal reactions for Mochi and Pip.

---

# 13. Technical Design

## Project Specifications

| Category | Specification |
|---|---|
| Unity Version | 6000.5.1f1 |
| Render Mode | 2D; exact render pipeline **TBD** |
| Target Platform | Windows PC |
| Target Resolution | 1920 × 1080, 16:9 |
| Target Frame Rate | 30 FPS |
| Camera | 2D side-scrolling |
| Physics | Unity 2D Physics |
| Input | Unity Input System recommended; implementation **TBD** |
| Save System | Manual Save (**Placeholder**) |

## Scene Structure

Recommended scene set:

- `Boot` – initializes persistent systems and loads menus.
- `MainMenu` – menu, settings, credits.
- `Level_01_Alley`.
- `Level_02_Factory`.
- `Level_03_Sewers`.
- `Ending` – optional; may remain inside Level 3 to reduce scope.

Persistent services should be limited to audio, settings, save data, and scene transitions. Avoid a large global manager containing unrelated gameplay responsibilities.

## Player Architecture

Use an explicit player state model for grounded, airborne, dashing, damaged, and defeated states. Input collection, movement calculations, animation presentation, and health/life handling should remain separable enough to tune without cross-system regressions.

Physics updates occur in `FixedUpdate`; raw input is read through actions and buffered for physics consumption. Use Rigidbody2D movement consistently rather than mixing transform movement with dynamic physics.

## Camera

The camera follows Mochi horizontally with a forward look-ahead and a small dead zone. Vertical movement is damped to prevent excessive motion during short jumps. Camera bounds prevent exposing areas outside authored levels. Screen shake is brief, low-amplitude, and optional.

## Save Data

**Manual Save (Placeholder):** The minimal save stores highest unlocked level, settings, and optional best results. Active checkpoint data may remain session-only. Use a small versioned local JSON file or Unity-supported serialization approach; do not store sensitive data.

If manual save UI cannot be completed safely, use automatic level-unlock persistence and relabel the menu accordingly. The game must never imply that progress was saved when it was not.

## Performance and Quality Targets

- Maintain 30 FPS at 1920×1080 on the agreed test PC.
- Use sprite atlases and shared materials where beneficial.
- Pool frequently spawned effects or projectiles if profiling shows allocation spikes.
- Limit parallax layer count and overdraw.
- Test collision at low and high frame rates.
- No blocking errors, progression breaks, missing references, or unhandled input-device states in the release build.

---

# 14. Asset List

## Art and Environment Packs

| Asset Pack | Intended Use | Integration Notes |
|---|---|---|
| Kenney Animal Pack | Mochi, Pip, enemy cats, enemy mice, and supporting character animation sources | Recolor or add accessories to distinguish roles; verify license attribution requirements. |
| Kenney Platformer Pack | Platforms, hazards, collectibles, props, and general level-building tiles | Standardize scale, colliders, pivots, and sorting layers before level production. |
| Kenney Platformer Buildings | Alley architecture, factory structures, pipes, rooftops, and urban props | Use modularly; adjust palette to preserve foreground readability. |
| Industrial Parallax Pack | Multi-layer alley/factory/sewer backgrounds and industrial depth | Keep noninteractive layers low contrast; profile overdraw and scrolling cost. |

## UI and Icon Packs

| Asset Pack | Intended Use | Integration Notes |
|---|---|---|
| Kenney UI Pack | Buttons, panels, sliders, toggles, selection states, and menu framing | Apply one consistent visual family and verify nine-slice settings. |
| Kenney Input Prompts | Keyboard and gamepad control prompts | Update prompts when active device changes if schedule allows. |
| Kenney Game Icons | Lives, coins, power cells, settings, pause, and results icons | Select a consistent outline weight and color treatment. |

## Repository Status

The following source archives are currently present in the project asset library:

- Kenney Animal Pack.
- Kenney New Platformer Pack 1.1.
- Kenney Platformer Art: Buildings.
- Kenney Game Icons.
- Industrial Parallax Pack.

The Kenney UI Pack and Kenney Input Prompts are planned dependencies but are **not yet confirmed in the current repository snapshot**. Import only the files needed for production and retain source/license documentation outside runtime asset folders where appropriate.

---

# 15. Team Structure

| Role | Count | Primary Responsibilities | Key Deliverables |
|---|---:|---|---|
| Programmer | 1 | Player controller, enemies, hazards, UI logic, save/settings, builds, integration | Stable playable build and technical systems |
| Designer | 2 | GDD ownership, level design, encounter design, tuning, asset integration support | Three levels, balance passes, design documentation |
| Sound Designer | 1 | Music, SFX, mixing, audio implementation support | Level music, gameplay cues, balanced mix |
| QA Tester | 1 | Test planning, regression, device/build validation, issue reporting | Test cases, prioritized bug reports, release verification |

Shared responsibilities include daily build checks, scope decisions, playtesting, and release packaging. One designer should act as production owner for milestone tracking and final scope calls.

---

# 16. Development Timeline

The schedule assumes development begins no later than July 2, 2026 and culminates in submission on July 31, 2026. Milestones overlap where work can proceed safely in parallel.

| Date | Milestone | Exit Criteria |
|---|---|---|
| July 2–5 | Prototype | Graybox test scene; responsive movement, jump, double jump, dash, collision, life loss, and checkpoint loop; mechanic values documented. |
| July 6–11 | Core Gameplay | Enemy and hazard framework functional; collectibles tracked; camera, level transitions, game over, and win flow playable; Alley graybox complete. |
| July 12–17 | Level Production & Art Integration | All three levels playable from start to finish; approved assets imported and standardized; core UI integrated; no missing critical content. |
| July 18–21 | Audio | Music and required SFX implemented; volume controls functional; mix readable during dense sequences. |
| July 22–25 | Testing | Full QA pass, difficulty and checkpoint tuning, controller/keyboard validation, 1920×1080 build test; all progression blockers resolved. |
| July 26–29 | Polishing | Animation/VFX feedback, UI cleanup, performance pass, credits, settings, final collectible placement; content lock by July 29. |
| July 30 | Release Candidate | Clean Windows build passes smoke and regression tests; documentation and package verified. |
| July 31 | Submission | Approved release build and required project files delivered before the deadline. |

## Scope Gates

- **July 5:** If movement is not stable, cut secondary dash interactions and focus on the baseline horizontal dash.
- **July 11:** If enemy combat is unreliable, convert enemies to avoidable hazards and remove stomp defeat.
- **July 17:** If all levels are not end-to-end playable, reduce level length before adding polish.
- **July 25:** No new features after this date; only fixes, tuning, accessibility, and presentation polish.

---

# 17. Risks

| Category | Risk | Impact / Likelihood | Mitigation |
|---|---|---|---|
| Technical | Player controller feels inconsistent across slopes, edges, and moving platforms. | High / Medium | Prototype first; use coyote time and buffered input; create focused movement test cases; avoid late physics rewrites. |
| Technical | Collision tunneling or unreliable enemy hits at dash speed. | High / Medium | Use appropriate Rigidbody2D collision detection, dedicated hitboxes, conservative dash speed, and frame-rate tests. |
| Technical | Save behavior becomes ambiguous or corrupts progress. | Medium / Medium | Keep saved data minimal and versioned; validate writes; provide safe defaults; cut manual save UI if incomplete. |
| Gameplay | Dash and double jump trivialize obstacles or make routes unreadable. | Medium / High | Establish movement metrics first; build levels against measured ranges; reserve safety margins; playtest without designer coaching. |
| Gameplay | Three lives create excessive repetition in a short game. | High / Medium | Use frequent checkpoints, short respawns, generous early sections, and adjust game-over checkpoint retention after testing. |
| Gameplay | Enemies distract from platforming or feel unfair. | Medium / Medium | Use clear anticipation, simple behaviors, and guaranteed avoidance paths; remove combat dependency if necessary. |
| Schedule | One programmer becomes the integration bottleneck. | High / High | Freeze interfaces early; designers build with prefabs; maintain daily playable builds; enforce scope gates. |
| Schedule | Three levels exceed the available production window. | High / Medium | Reuse modular systems, target 5–8 minute levels, cut length before quality, and combine rather than add mechanics. |
| Asset | Mixed packs create inconsistent scale, palette, or visual language. | Medium / High | Create an import/style guide, standardize pixels per unit, apply shared palette adjustments, and approve prefabs before mass placement. |
| Asset | Planned UI/input packs are unavailable or licensing documentation is missing. | Medium / Medium | Confirm dependencies during prototype week; substitute simple in-engine UI; retain license and attribution records. |

---

# 18. Future Improvements

The following ideas are outside the July 31 release scope and should be reconsidered only after the core game is complete:

- Time-trial mode with medals and ghost replays.
- Additional levels such as rooftops, market district, or rail yard.
- Alternate playable cats with distinct movement traits.
- Expanded enemy behaviors and a nonviolent Pip confrontation set piece.
- Secret routes and completion rewards.
- Additional accessibility and difficulty assists.
- Full input rebinding and broader controller support.
- Adaptive music and richer environmental ambience.
- Collectible power cells that modify dash behavior.
- Localization and expanded platform support.
- Achievements, leaderboards, or challenge modes.

---

# Appendix

## A. Game Flow Diagram (Text)

```text
[Launch]
   |
   v
[Boot / Load Settings]
   |
   v
[Main Menu] <-----------------------------+
   |                                      |
   +--> [Settings / Credits] --Back-------+
   |
   v
[Level 1: Alley]
   |  failure with lives remaining -> [Checkpoint Respawn]
   |  no lives -> [Game Over] -> [Restart Level / Main Menu]
   v
[Level 2: Factory]
   |  failure rules repeat
   v
[Level 3: Sewers]
   |
   v
[Catch Pip / Recover Fish Skeleton]
   |
   v
[Victory Screen]
   |
   +--> [Replay Game]
   +--> [Main Menu]
```

## B. Gameplay Loop Diagram (Text)

```text
[See Pip / Next Goal]
          |
          v
[Read Platforms, Hazards, and Enemies]
          |
          v
[Move + Jump + Double Jump + Dash]
          |
          +----> [Optional Collectible Route] ----+
          |                                       |
          v                                       |
[Avoid or Overcome Encounter] <-------------------+
          |
          +---- failure ----> [Lose Life]
          |                         |
          |                         v
          |                  [Checkpoint Respawn]
          |                         |
          +-------------------------+
          |
          v
[Activate Checkpoint / Reach Exit]
          |
          v
[Next Challenge or Level]
```

## C. References

- **Geometry Dash** – reference for energetic pacing, immediate readability, and compact obstacle escalation. Alley Fursuit differs through direct horizontal control, exploratory collectible paths, non-rhythm movement, and a broader platforming move set.
- **Kenney Animal Pack** – character art source.
- **Kenney Platformer Pack** – platform, prop, hazard, and collectible art source.
- **Kenney Platformer Buildings** – modular environment art source.
- **Kenney UI Pack** – planned menu and HUD source.
- **Kenney Input Prompts** – planned input glyph source.
- **Kenney Game Icons** – HUD and menu icon source.
- **Industrial Parallax Pack** – layered background source.
- Unity 6.5 / Unity 2D Physics – runtime and physics framework.

---

*End of document.*
