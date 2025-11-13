import java.util.LinkedList;
import java.util.Queue;

public class LinkedlistKuyruk {
    private Queue<String> oncelikli1 = new LinkedList<>();
    private Queue<String> oncelikli2 = new LinkedList<>();
    private Queue<String> oncelikli3 = new LinkedList<>();

    public void ekle(Musteri musteri,int oncelik){

        if (oncelik==1){
            oncelikli1.add(musteri.getAd());
        }else if(oncelik==2){
            oncelikli2.add(musteri.getAd());
        }else if (oncelik==3){
            oncelikli3.add(musteri.getAd());
        }
    }
    public void siradanCikar(){
        if(!oncelikli1.isEmpty()){
            oncelikli1.remove(0);
            System.out.println("birincil oncelikli siradan cikarildi..");
        }else if(!oncelikli2.isEmpty()){
            oncelikli2.remove(0);
            System.out.println("ikincil oncelikli siradan cikarildi..");
        }else if(!oncelikli3.isEmpty()){
            oncelikli3.remove(0);
            System.out.println("ucuncul oncelikli siradan cikarildi..");
        }
        else {
            System.out.println("kuyruk bos.");
        }

    }
    public void listele(){
        System.out.println("oncelikli 1 liste:");
        System.out.println(oncelikli1.toString());
        System.out.println("oncelikli 2 liste:");
        System.out.println(oncelikli2.toString());
        System.out.println("oncelikli 3 liste:");
        System.out.println(oncelikli3.toString());

    }
}
