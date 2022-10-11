import { Component, OnInit } from '@angular/core';
import { CreateNewTask, TaskFormModel, TaskModel } from '../shared/models/task.models';
import { TasksService } from '../shared/services/tasks.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit {

  public tasks: TaskModel[] = [];
  public addTaskForm: FormGroup;
  public loading: boolean = false;

  constructor(private tasksSvc: TasksService, private modalService: NgbModal) { 
    this.addTaskForm = new FormGroup({
      addTaskFormModalTitle: new FormControl('', Validators.minLength(3)),
      addTaskFormModalDescription: new FormControl('', Validators.minLength(10))
    });
  }

  ngOnInit(): void {
    this.tasksSvc.getRootTasks().subscribe((tasks) => {
      this.tasks = tasks;
    });
  }

  public addTaskSubmit(){
    const createTaskModel:CreateNewTask = {
      title: this.addTaskForm.get('addTaskFormModalTitle')?.value,
      description: this.addTaskForm.get('addTaskFormModalDescription')?.value,
      parentId: undefined,
    };
    this.loading = true;
    this.tasksSvc.createTask(createTaskModel).subscribe({next: (newTask) => {
      this.modalService.dismissAll("Create Task");
      this.addTaskForm.reset();
      this.loading = false;
      this.tasks.push(newTask);
    }, 
    error: () => {
      this.loading = false;
    }});
  }
  
  public open(content: any){
    this.modalService.open(content)
    .result.then((result) => {
      console.log(result);
    }, (reason) => {

    });
  }

}
