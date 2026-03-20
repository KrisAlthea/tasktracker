# Task Tracker 使用说明

## 概述

[Task Tracker](https://roadmap.sh/projects/task-tracker) 是一个命令行任务管理工具，支持任务的增删改查、状态管理及数据持久化存储。

---

## 快速开始

### 启动程序

```bash
cd tasktracker
dotnet run
```

### 退出程序

输入 `exit` 或 `quit` 即可退出。

---

## 命令列表

| 命令             | 语法                    | 说明                     |
| ---------------- | ----------------------- | ------------------------ |
| add              | `add "描述"`            | 添加新任务               |
| update           | `update <id> "新描述"`  | 更新任务描述             |
| delete           | `delete <id>`           | 删除任务                 |
| mark-in-progress | `mark-in-progress <id>` | 标记为进行中             |
| mark-done        | `mark-done <id>`        | 标记为已完成             |
| list             | `list [状态]`           | 列出任务（可选过滤状态） |
| help             | `help`                  | 显示帮助信息             |
| exit             | `exit`                  | 退出程序                 |

---

## 使用示例

### 添加任务

```
> add "Buy groceries"
Task added successfully (ID: 1)

> add "Clean house"
Task added successfully (ID: 2)
```

### 查看任务

```
> list
| ID  | Status | Description   |
| --- | ------ | ------------- |
| 1   | todo   | Buy groceries |
| 2   | todo   | Clean house   |
```

### 按状态筛选

```
> list todo        # 只显示待办任务
> list in-progress # 只显示进行中任务
> list done        # 只显示已完成任务
```

### 更新任务

```
> update 1 "Buy groceries and cook dinner"
Task 1 updated successfully.
```

### 修改状态

```
> mark-in-progress 1
Task 1 marked as in-progress.

> mark-done 1
Task 1 marked as done.
```

### 删除任务

```
> delete 2
Task 2 deleted successfully.
```

---

## 任务状态

| 状态          | 说明             |
| ------------- | ---------------- |
| `todo`        | 待办（默认状态） |
| `in-progress` | 进行中           |
| `done`        | 已完成           |

---

## 数据存储

- 数据保存在程序运行目录下的 `tasks.json` 文件
- 所有修改自动保存，重启程序后数据保留

---

## 项目结构

```
tasktracker/
├── Program.cs        # 命令解析与交互循环
├── TaskManager.cs    # 任务管理与数据持久化
├── MyTask.cs         # 任务实体与状态枚举
├── StatusConverter.cs # JSON 状态转换器
└── tasks.json        # 数据文件（运行时生成）
```

---

## 系统要求

- .NET 8.0 SDK
- 跨平台支持 (Windows / Linux / macOS)