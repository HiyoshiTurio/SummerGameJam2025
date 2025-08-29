# Summer Game Jam 2025 - Restaurant Rush 🍜

[English](#english) | [中文](#中文)

---

## English

### What is this project? (Space Home)
This Unity project is a **competitive multiplayer restaurant serving game** created for Summer Game Jam 2025. Despite the "space home" reference in the inquiry, this is actually a restaurant simulation game where two players compete to serve customers efficiently in a fast-paced environment.

### 🎮 Game Overview
**Restaurant Rush** is a local multiplayer game where two players work as restaurant servers, competing to earn the highest score by delivering food and drinks to customers. Players must manage customer satisfaction, handle unexpected troubles, and work efficiently under time pressure.

### 🌟 Key Features
- **Two-Player Local Multiplayer**: Competitive split-screen gameplay
- **Customer Service System**: Serve food to hungry customers
- **Trouble Resolution**: Handle customer complaints with drinks
- **Time Management**: Race against the clock in timed matches
- **Movement Mechanics**: Walk and dash abilities for quick service
- **Score Competition**: Earn points for successful deliveries
- **Audio Experience**: Background music and sound effects
- **Japanese Restaurant Theme**: Traditional setting with authentic elements

### 🕹️ Controls

#### Player 1
- **Movement**: Arrow Keys / WASD
- **Action/Pickup**: Left Shift
- **Dash**: Left Shift (while moving)

#### Player 2  
- **Movement**: Numeric Keypad / Custom assigned keys
- **Action/Pickup**: Right Shift
- **Dash**: Right Shift (while moving)

#### Debug Controls (Development)
- **A Key**: Add 5 points to Player 1
- **D Key**: Add 5 points to Player 2

### 🎯 Gameplay Mechanics

1. **Food Generation**: Food items spawn at generator stations
2. **Customer Service**: Pick up food and deliver to waiting customers
3. **Customer States**:
   - 🍽️ **Hungry**: Waiting for food
   - 😋 **Eating**: Consuming delivered food
   - 😠 **Trouble**: Needs drink to resolve issues
4. **Scoring System**:
   - +100 points for successful food delivery
   - +50 points for resolving customer troubles
5. **Time Pressure**: Complete as many deliveries as possible within the time limit

### 🛠️ Development Setup

#### Requirements
- Unity 2022.3 LTS or later
- Windows/Mac/Linux development environment

#### How to Run
1. Clone this repository
2. Open the project in Unity
3. Load the main game scene: `Assets/Scenes/SampleScene.unity` or `Assets/Tomiyama/InGame.unity`
4. Press Play to start the game
5. For title screen, load: `Assets/Ito/Title.unity`

### 📁 Project Structure
```
Assets/
├── Scenes/           # Game scenes
├── Hiyoshi/          # Audio system and debug tools
├── Tomiyama/         # Core gameplay mechanics and customer AI
├── Kobayashi/        # UI and scoring system
├── Ro/               # Player movement and controls
├── Ito/              # Scene management and title screen
├── kanda/            # Game manager and state control
└── Resources/        # Game assets and materials
```

### 👥 Development Team
- **Hiyoshi**: Audio systems and sound design
- **Tomiyama**: Customer behavior and food generation systems
- **Kobayashi**: UI design and scoring mechanics
- **Ro**: Player movement and physics
- **Ito**: Scene management and flow
- **Kanda**: Game management and state control

### 🎵 Audio Features
- Background music during gameplay
- Sound effects for actions (food pickup, delivery, scoring)
- Footstep audio system
- Dynamic audio management

---

## 中文

### 这个项目是什么？(Space Home)
这个Unity项目是为**2025夏季游戏制作比赛**创建的**竞技多人餐厅服务游戏**。尽管询问中提到了"space home"，但这实际上是一个餐厅模拟游戏，两名玩家在快节奏的环境中竞争，看谁能更有效地为顾客服务。

### 🎮 游戏概述
**餐厅冲刺**是一款本地多人游戏，两名玩家扮演餐厅服务员，通过为顾客提供食物和饮料来竞争获得最高分数。玩家必须管理顾客满意度，处理意外问题，并在时间压力下高效工作。

### 🌟 主要特点
- **双人本地多人游戏**: 竞技分屏游戏体验
- **顾客服务系统**: 为饥饿的顾客提供食物
- **问题解决**: 用饮料处理顾客投诉
- **时间管理**: 在限时比赛中与时间赛跑
- **移动机制**: 行走和冲刺能力，提供快速服务
- **分数竞争**: 通过成功送餐获得积分
- **音频体验**: 背景音乐和音效
- **日式餐厅主题**: 传统设定与真实元素

### 🕹️ 操作控制

#### 玩家1
- **移动**: 方向键 / WASD
- **行动/拾取**: 左Shift
- **冲刺**: 左Shift（移动时）

#### 玩家2
- **移动**: 数字小键盘 / 自定义分配键
- **行动/拾取**: 右Shift  
- **冲刺**: 右Shift（移动时）

#### 调试控制（开发用）
- **A键**: 为玩家1添加5分
- **D键**: 为玩家2添加5分

### 🎯 游戏机制

1. **食物生成**: 食物道具在生成器站点产生
2. **顾客服务**: 拾取食物并送给等待的顾客
3. **顾客状态**:
   - 🍽️ **饥饿**: 等待食物
   - 😋 **用餐**: 正在享用送达的食物
   - 😠 **有问题**: 需要饮料来解决问题
4. **计分系统**:
   - 成功送餐获得+100分
   - 解决顾客问题获得+50分
5. **时间压力**: 在时间限制内完成尽可能多的送餐

### 🛠️ 开发设置

#### 系统要求
- Unity 2022.3 LTS或更高版本
- Windows/Mac/Linux开发环境

#### 运行方法
1. 克隆此仓库
2. 在Unity中打开项目
3. 加载主游戏场景: `Assets/Scenes/SampleScene.unity` 或 `Assets/Tomiyama/InGame.unity`
4. 按播放键开始游戏
5. 标题界面请加载: `Assets/Ito/Title.unity`

### 📁 项目结构
```
Assets/
├── Scenes/           # 游戏场景
├── Hiyoshi/          # 音频系统和调试工具
├── Tomiyama/         # 核心游戏机制和顾客AI
├── Kobayashi/        # UI和计分系统
├── Ro/               # 玩家移动和控制
├── Ito/              # 场景管理和标题界面
├── kanda/            # 游戏管理器和状态控制
└── Resources/        # 游戏资源和材质
```

### 👥 开发团队
- **Hiyoshi**: 音频系统和声音设计
- **Tomiyama**: 顾客行为和食物生成系统
- **Kobayashi**: UI设计和计分机制
- **Ro**: 玩家移动和物理
- **Ito**: 场景管理和流程
- **Kanda**: 游戏管理和状态控制

### 🎵 音频特性
- 游戏过程中的背景音乐
- 动作音效（食物拾取、送餐、计分）
- 脚步声音频系统
- 动态音频管理

---

### 📜 License
This project was created for Summer Game Jam 2025. Please respect the intellectual property of the development team.

### 🚀 Getting Started
Ready to start serving customers? Load up the game and show off your restaurant serving skills! The kitchen is waiting! 🍜👨‍🍳👩‍🍳