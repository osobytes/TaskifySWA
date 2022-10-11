export interface TaskModel {
    id: string;
    parentTask?: string;
    title: string;
    description: string;
    createdBy: string;
    creationDate: Date;
}

export interface TaskDetails{
    task: TaskModel;
    childTasks: TaskModel[];
}

export interface CreateNewTask{
    title: string;
    description: string;
    parentId?: string;
}

export interface UpdateTask{
    key: TaskKey;
    title: string;
    description: string;
}

export interface SetParentTask{
    key: TaskKey;
    newParentId: string;
}

export interface TaskKey{
    id: string;
    parentId?: string;
}

export interface TaskFormModel{
    title: string;
    description: string;
}