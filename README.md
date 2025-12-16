# Cosmic Bowling

Cosmic Bowling is a Unity-based bowling simulation developed using **Reinforcement Learning** with the **ML-Agents** library.  
The project focuses on training an autonomous agent to learn optimal bowling strategies through reward-based learning.

## Project Overview

During this project, a bowling simulation was implemented in Unity, where an intelligent agent learns how to perform a **strike** by maximizing its reward.  
The agent was trained to:
- Avoid obstacles placed along the lane
- Optimize its actions to maximize the final reward
- Adapt its behavior through Reinforcement Learning techniques

The game is structured across **multiple levels with increasing difficulty**, allowing a detailed analysis of the agent’s learning behavior based on changes in training conditions.

## Technologies & Tools
- **Language:** C#
- **Game Engine:** Unity
- **Reinforcement Learning:** Unity ML-Agents
- **IDE:** Visual Studio

## Reinforcement Learning Details

- Agent actions are defined using the ML-Agents framework
- Training is based on **reward mechanisms**
- Performance is evaluated through:
  - Learning curves
  - Training time
  - Success rate in achieving a strike
- Different **hyperparameter configurations** were tested to study their impact on the agent’s learning process

## Installation & Setup

To run the project locally:

```bash
# Clone the repository
git clone https://github.com/rosros98/CosmicBowling.git
```

1. Open **Unity Hub**
2. Click **Add** and select the cloned `CosmicBowling` folder
3. Open the project using a compatible Unity version
4. Make sure **ML-Agents** is installed and configured
5. Press **Play** in the Unity Editor to run the simulation

## Project Structure

```
Assets/              # Game assets, ML-Agents scripts, scenes
Packages/            # Unity package dependencies
ProjectSettings/     # Unity project settings
```
