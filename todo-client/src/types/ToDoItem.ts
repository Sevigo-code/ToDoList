export interface ToDoItem {
  id: number;
  title: string;
  description?: string;
  maxCompletionDate: string;
  isCompleted: boolean;
  createdAt: string;
}

export interface CreateToDoItem {
  title: string;
  description?: string;
  maxCompletionDate: string;
}

export interface UpdateToDoItem {
  title: string;
  description?: string;
  maxCompletionDate: string;
  isCompleted: boolean;
}
