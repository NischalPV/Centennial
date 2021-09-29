#include<stdio.h>

int main()
{
    int a[10],i,len,temp;
    
    printf("Enter len");
    scanf("%d",&len);
    
    for(i=0;i<len-1;i++)
    {
                        printf("Enter element");
                        scanf("%d",&a[i]);
    }
    
    for(i=0;i<len-1;i++)
    {
                        if(a[i]>a[i+1])
                        {
                                       temp=a[i];
                                       a[i]=a[i+1];
                                       a[i+1]=temp;
                                       i=-1;
                        }
    }
    
    for(i=0; i<len-1; i++)
    {
             printf("%d\t",a[i]);
    }
}
