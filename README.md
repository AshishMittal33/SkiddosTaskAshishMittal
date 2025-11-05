https://github.com/user-attachments/assets/23ea1e4c-eed8-44b1-aeab-82ede3782bd2

# 🐦 Bird vs Pig – Unity 2D Mini Game

A fun 2D Unity project where a bird chases a pig across a forest path with smooth parallax, tap-based movement, progress tracking, and reward screen.

# 🎮 Game Overview

Bird starts on the left, Pig on the right.

Tap to move — each tap brings them closer until they meet in the center.

Both bounce during the chase to show motion.

Parallax background with 3 layers:

Front (fast bushes)

Middle (ground)

Rear (slow bushes)

# 📊 Progress System

Progress bar at the top fills with each tap.

Reaches 100% → Bird catches Pig → Reward screen appears.

Number of taps required is configurable in the Inspector.

# 🎵 Audio & FX

Continuous BGM during chase → fades out at victory.

Hop SFX, Defeat SFX, and Happy SFX for characters.

Trail when bird hops and dust burst when landing on pig.

# 🧩 UI Flow

Start Screen: Play button → begins chase.

Game Scene: Bird & Pig chase + progress bar.

Reward Screen: “You Won!” + Back button → Start screen.

# 🕹️ Gameplay Summary

Tap → Bird moves right, Pig moves left.

After X taps → Bird jumps on Pig’s head.

Pig squishes, BGM fades → Reward screen shows.

# ⚙️ Core Scripts
Script	Function
GameManager	Tap logic, win flow
CharacterMover	Movement interpolation
ProgressBarController	Progress bar fill
BirdAnimator	Hop + victory animation
PigAnimator	Hop + defeat animation
ParallaxLayer	Background scrolling
ButtonScenePop	UI entry pop effect
# 🛠️ Tech Used

Unity 6000.2.6f2

C#

Particle System

Unity UI


