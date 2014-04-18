PicoDilly
=========

Simple and short implementation of IoC container. 

It does not have:
  - thread safety
  - life time management
  - probably many other useful and cool features.
  
Yet it can be configured in similar to StructureMap way:

            _container = new Container();
            _container.Configure(reg => {
                reg.For<IQueueManager>().Use(qm);
                reg.For<IEventAgregator>().Use((ea));
                reg.For<GameController>().Use(this);
            });

It was written as a simple and short replacement for Autofac (uses labmda advanced features which are not supported in current version of mono) and StructureMap (which at some place references System.Web which is not implemented in mono).
