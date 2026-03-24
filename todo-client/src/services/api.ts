import axios from 'axios';
import type { ToDoItem, CreateToDoItem, UpdateToDoItem } from '../types/ToDoItem';

const API_URL = 'http://localhost:5299/api/todos';

const api = axios.create({
  baseURL: API_URL,
});

export const todoService = {
  getAll: () => api.get<ToDoItem[]>(''),
  getById: (id: number) => api.get<ToDoItem>(`/${id}`),
  create: (data: CreateToDoItem) => api.post<ToDoItem>('', data),
  update: (id: number, data: UpdateToDoItem) => api.put<ToDoItem>(`/${id}`, data),
  delete: (id: number) => api.delete(`/${id}`),
};
