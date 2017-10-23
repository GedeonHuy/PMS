import { Injectable, EventEmitter } from '@angular/core';
import { SystemConstants } from './../common/system.constants';
import { AuthenService } from './authen.service';
import { HubConnection } from '@aspnet/signalr-client';

@Injectable()
export class SignalrService {

  // Declare the variables  
  private proxy: any;
  private proxyName: string = 'hub';
  private connection: any;
  // create the Event Emitter  
  public send: EventEmitter<any>;

  public connectionEstablished: EventEmitter<Boolean>;
  public connectionExists: Boolean;

  constructor(private _authenService: AuthenService) {
    // Constructor initialization  
    this.connectionEstablished = new EventEmitter<Boolean>();
    this.send = new EventEmitter<any>();
    this.connectionExists = false;
    // create hub connection  
    this.connection = new HubConnection (SystemConstants.BASE_URL + "/" + this.proxyName);
    // register on server events  
    this.registerOnServerEvents();
    // call the connecion start method to start the connection to send and receive events.  
    this.startConnection();
  }
  // check in the browser console for either signalr connected or not  
  private startConnection(): void {
    this.connection.start()
      .then(() => {
        console.log('Hub connection started')
      })
      .catch(err => {
        console.log('Error while establishing connection')
      });
  }

  private registerOnServerEvents(): void {
    
    this.connection.on('Send', (data: any) => {
      this.send.emit(data);      
    });
  }
}